using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Toolkit.Uwp.Notifications;

namespace EyeRest
{
    // ApplicationContext that manages the tray icon and periodic reminders.
    public class EyeRestApplicationContext : ApplicationContext
    {
        // Session-configurable reminder interval in minutes. Default 20.
        // This is intentionally not persisted to disk unless RememberSettings is true.
        public static int ReminderIntervalMinutes = 20;

        // New session-only flag: whether left-click toggles reminders. Default: false to keep behavior predictable.
        public static bool UseLeftClickToggle = false;

        // New flags for persistence and autostart
        public static bool RememberSettings = false;
        public static bool StartWithWindows = false;

        // Static icons used for the tray icon states. Do NOT dispose these anywhere.
        private static readonly Icon EnabledIcon = EyeRest.Properties.Resources.AppIcon;
        private static readonly Icon SnoozedIcon = EyeRest.Properties.Resources.AppSnoozeIcon;

        // The NotifyIcon that appears in the system tray.
        NotifyIcon notifyIcon = new NotifyIcon();

        // Configuration form created lazily when user opens settings.
        Configuration configWindow = null; // create lazily

        // System.Threading.Timer that triggers periodic reminders on a ThreadPool thread.
        System.Threading.Timer myTimer;

        // Lightweight control created on the UI thread to marshal callbacks to the UI thread.
        Control uiInvoker;

        // A short WinForms timer used to distinguish single-click from double-click on the tray icon.
        private System.Windows.Forms.Timer singleClickConfirmTimer;

        public EyeRestApplicationContext()
        {
            // Create menu items: Options…, About…, Exit
            MenuItem optionsMenuItem = new MenuItem("Options…", new EventHandler(ShowConfig));
            MenuItem aboutMenuItem = new MenuItem("About…", new EventHandler(ShowAbout));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            // Set initial icon from static EnabledIcon
            notifyIcon.Icon = EnabledIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { optionsMenuItem, aboutMenuItem, exitMenuItem });
            notifyIcon.Visible = true;

            // Create UI invoker control on the UI thread for marshaling.
            uiInvoker = new Control();
            try
            {
                // Force handle creation so BeginInvoke works.
                IntPtr tmp = uiInvoker.Handle;
            }
            catch
            {
                // swallow
            }

            // Create a timer but do not start it yet.
            myTimer = new System.Threading.Timer(ShowMessageCallback, null, Timeout.Infinite, Timeout.Infinite);

            // Default in-memory setting
            try { EyeRest.Properties.Settings.Default.ShowMessage = true; } catch { }

            // Load persistent settings if present
            try
            {
                string section = "General";
                bool remember = EyeRest.IniSettingsHelper.ReadBool(section, "RememberSettings", false);
                if (remember)
                {
                    RememberSettings = true;
                    EyeRest.Properties.Settings.Default.ShowMessage = EyeRest.IniSettingsHelper.ReadBool(section, "ShowMessage", EyeRest.Properties.Settings.Default.ShowMessage);
                    ReminderIntervalMinutes = EyeRest.IniSettingsHelper.ReadInt(section, "ReminderIntervalMinutes", ReminderIntervalMinutes);
                    UseLeftClickToggle = EyeRest.IniSettingsHelper.ReadBool(section, "UseLeftClickToggle", UseLeftClickToggle);
                    StartWithWindows = EyeRest.IniSettingsHelper.ReadBool(section, "StartWithWindows", StartWithWindows);

                    // Apply autostart if enabled
                    try
                    {
                        if (StartWithWindows)
                        {
                            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                            EyeRest.AutoStartHelper.SetAutoStart(true, exePath);
                        }
                    }
                    catch { }
                }
                else
                {
                    RememberSettings = false;
                }
            }
            catch { }

            // Initialize notification state based on the (possibly loaded) setting
            UpdateNotificationState();

            // Wire mouse events. Right-click still shows context menu automatically.
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }

        // Show About dialog (modal)
        private void ShowAbout(object sender, EventArgs e)
        {
            try
            {
                using (var about = new AboutForm())
                {
                    about.ShowDialog();
                }
            }
            catch
            {
                // swallow to avoid crashing the tray app
            }
        }

        // Apply persisted setting: start or stop timer and update icon/tooltip.
        private void UpdateNotificationState()
        {
            bool enabled = EyeRest.Properties.Settings.Default.ShowMessage;
            try
            {
                if (enabled)
                {
                    // restore app icon and tooltip
                    notifyIcon.Icon = EnabledIcon;
                    notifyIcon.Text = "EyeRest - Rest Reminder";
                    StartTimer();
                }
                else
                {
                    // change tooltip and icon to indicate disabled notifications
                    try { notifyIcon.Icon = SnoozedIcon; } catch { }
                    notifyIcon.Text = "EyeRest - notifications disabled";
                    StopTimer();
                }
            }
            catch { /* don't let notification state failures crash app */ }
        }

        private void StartTimer()
        {
            try
            {
#if DEBUG
                int intervalMs = 5000; // 5 seconds for debugging
#else
                int intervalMs = Math.Max(1, ReminderIntervalMinutes) * 60 * 1000;
#endif
                myTimer?.Change(intervalMs, intervalMs);
            }
            catch { /* ignore timer start failures */ }
        }

        private void StopTimer()
        {
            try
            {
                myTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            }
            catch { /* ignore timer stop failures */ }
        }

        // Timer callback (runs on ThreadPool). Marshal UI work to UI thread.
        void ShowMessageCallback(object state)
        {
            bool enabled;
            try
            {
                enabled = EyeRest.Properties.Settings.Default.ShowMessage;
            }
            catch
            {
                StopTimer();
                return;
            }

            if (!enabled)
            {
                StopTimer();
                return;
            }

            try
            {
                uiInvoker.BeginInvoke((Action)(() =>
                {
                    try
                    {
                        ShowToastReminder();
                    }
                    catch
                    {
                        // swallow UI exceptions
                    }
                }));
            }
            catch
            {
                // swallow marshaling failures
            }
        }

        // Build and display a Windows 10/11 toast, falling back to balloon tip if necessary.
        private void ShowToastReminder()
        {
            try
            {
                // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package.
                new ToastContentBuilder()
                    .AddText("EyeRest reminder")
                    .AddText("You should take rest for at least 20 seconds!")
                    .Show();
            }
            catch
            {
                try { notifyIcon.ShowBalloonTip(3000, "EyeRest reminder", "You should take rest for at least 20 seconds!", ToolTipIcon.Info); } catch { }
            }
        }

        // Tray icon single-click handler: start a short confirm timer to ensure this is not a double-click.
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            // If there's a pending confirm timer, restart it.
            if (singleClickConfirmTimer == null)
            {
                singleClickConfirmTimer = new System.Windows.Forms.Timer();
                // wait slightly longer than system double-click time so double-click cancels single-click action
                singleClickConfirmTimer.Interval = Math.Max(50, SystemInformation.DoubleClickTime + 20);
                singleClickConfirmTimer.Tick += SingleClickConfirmTimer_Tick;
            }
            else
            {
                singleClickConfirmTimer.Stop();
            }

            singleClickConfirmTimer.Start();
        }

        // If the confirm timer ticks, treat the click as a confirmed single-click and either toggle reminders
        // or open the Options dialog depending on the session-only UseLeftClickToggle flag.
        private void SingleClickConfirmTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                singleClickConfirmTimer.Stop();
            }
            catch { }

            try
            {
                if (UseLeftClickToggle)
                {
                    // Toggle the in-memory ShowMessage setting.
                    bool current = EyeRest.Properties.Settings.Default.ShowMessage;
                    EyeRest.Properties.Settings.Default.ShowMessage = !current;

                    // Apply new state to UI and timer.
                    UpdateNotificationState();

                    // Show a brief confirmation via toast (fallback to balloon).
                    try
                    {
                        ShowToggleNotification(EyeRest.Properties.Settings.Default.ShowMessage);
                    }
                    catch
                    {
                        // swallow - helper already falls back, but be defensive
                    }
                }
                else
                {
                    // If left-click toggle is disabled, open the Options dialog on single-click for discoverability.
                    // This keeps left-click useful while avoiding unexpected toggles. Double-click still opens Options too.
                    ShowConfig(this, EventArgs.Empty);
                }
            }
            catch
            {
                // swallow
            }
        }

        // Double-click handler: cancel pending single-click action and open configuration.
        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (singleClickConfirmTimer != null)
                {
                    try { singleClickConfirmTimer.Stop(); } catch { }
                }
            }
            catch { }

            ShowConfig(sender, e);
        }

        // Show configuration dialog. Created lazily and disposed after close.
        void ShowConfig(object sender, EventArgs e)
        {
            if (configWindow != null)
            {
                if (configWindow.Visible)
                    configWindow.Focus();
                else
                    configWindow.ShowDialog();
                return;
            }

            configWindow = new Configuration();
            // ensure the dialog opens centered (designer already sets StartPosition.CenterScreen)
            configWindow.FormClosed += (s, args) =>
            {
                // when closed, apply settings and clear reference
                UpdateNotificationState();
                configWindow.Dispose();
                configWindow = null;
            };

            configWindow.ShowDialog();
        }

        // Exit menu action: clean up resources and end context.
        void Exit(object sender, EventArgs e)
        {
            // Clean up and exit the application context gracefully.
            CleanUpAndExit();
        }

        // Stop timers, remove icon and exit this ApplicationContext's message loop.
        // call from UI thread (e.g. Exit handler)
        private void CleanUpAndExit()
        {
            try { StopTimer(); } catch { }

            try
            {
                if (myTimer != null)
                {
                    // prevent further callbacks and wait up to 5s for running callbacks to finish
                    var waitHandle = new System.Threading.ManualResetEvent(false);
                    try
                    {
                        myTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    }
                    catch { }
                    try
                    {
                        myTimer.Dispose(waitHandle);
                        // wait for any running callback to finish (avoid blocking indefinitely)
                        waitHandle.WaitOne(5000);
                    }
                    catch { }
                    finally
                    {
                        try { waitHandle.Dispose(); } catch { }
                    }
                    myTimer = null;
                }
            }
            catch { }

            try { notifyIcon.Visible = false; notifyIcon.Dispose(); } catch { }
            try { uiInvoker?.Dispose(); } catch { }

            try { singleClickConfirmTimer?.Stop(); singleClickConfirmTimer?.Dispose(); } catch { }

            // finally exit message loop for this ApplicationContext
            ExitThread();
        }

        private void ShowToggleNotification(bool enabled)
        {
            string title = "EyeRest";
            string body = enabled ? "Reminders enabled" : "Reminders disabled";

            // Try toast first, fall back to balloon tip on any failure.
            try
            {
                // Requires Microsoft.Toolkit.Uwp.Notifications.
                new ToastContentBuilder()
                    .AddText(title)
                    .AddText(body)
                    .Show();
            }
            catch
            {
                try
                {
                    notifyIcon.ShowBalloonTip(3000, title, body, ToolTipIcon.Info);
                }
                catch
                {
                    // swallow - nothing more to do
                }
            }
        }
    }
}