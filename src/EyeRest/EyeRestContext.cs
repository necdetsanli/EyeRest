using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Toolkit.Uwp.Notifications;

namespace EyeRest
{
    // ApplicationContext that manages the tray icon and periodic reminders.
    public class EyeRestApplicationContext : ApplicationContext
    {
        // The NotifyIcon that appears in the system tray.
        NotifyIcon notifyIcon = new NotifyIcon();

        // Configuration form created lazily when user opens settings.
        Configuration configWindow = null; // create lazily

        // System.Threading.Timer that triggers periodic reminders on a ThreadPool thread.
        System.Threading.Timer myTimer;

        // Lightweight control created on the UI thread to marshal callbacks to the UI thread.
        Control uiInvoker;

        // Interval in milliseconds between reminders.
        readonly int timerIntervalMs;

        public EyeRestApplicationContext()
        {
            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon.Icon = EyeRest.Properties.Resources.AppIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;

#if DEBUG
            timerIntervalMs = 5000; // 5 seconds for debugging
#else
            timerIntervalMs = 1200000; // 20 minutes in release
#endif

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

            // Ensure the app defaults to showing notifications on each startup.
            // This sets the in-memory setting only and does not persist the change.
            try { EyeRest.Properties.Settings.Default.ShowMessage = true; } catch { }

            // Initialize notification state based on the (now-reset) setting
            UpdateNotificationState();

            // double-click tray icon to open configuration
            notifyIcon.DoubleClick += (s, e) => ShowConfig(s, e);
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
                    notifyIcon.Icon = EyeRest.Properties.Resources.AppIcon;
                    notifyIcon.Text = "EyeRest - Rest Reminder";
                    StartTimer();
                }
                else
                {
                    // change tooltip and icon to indicate disabled notifications
                    try { notifyIcon.Icon = SystemIcons.Application; } catch { }
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
                myTimer?.Change(timerIntervalMs, timerIntervalMs);
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

            // finally exit message loop for this ApplicationContext
            ExitThread();
        }
    }
}