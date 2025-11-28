using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EyeRest;


namespace EyeRest
{
    // ApplicationContext that manages the tray icon and periodic reminders.
    public class EyeRestApplicationContext : ApplicationContext
    {
        // The NotifyIcon that appears in the system tray.
        NotifyIcon notifyIcon = new NotifyIcon();
        // Configuration form created lazily when user opens settings.
        Configuration configWindow = null; // create lazily

        // The timer that triggers periodic reminders.
        Timer myTimer = new Timer();

        public EyeRestApplicationContext()
        {
            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon.Icon = EyeRest.Properties.Resources.AppIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;

            myTimer.Tick += new EventHandler(ShowMessage);
#if DEBUG
            // Short interval for testing in Debug builds
            myTimer.Interval = 5000; // 5 seconds
#else
            myTimer.Interval = 1200000; // 20 minutes
#endif

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
                    myTimer.Start();
                }
                else
                {
                    // change tooltip and icon to indicate disabled notifications
                    try { notifyIcon.Icon = SystemIcons.Application; } catch { }
                    notifyIcon.Text = "EyeRest - notifications disabled";
                    myTimer.Stop();
                }
            }
            catch { /* don't let notification state failures crash app */ }
        }

        // Timer tick handler: show balloon if enabled, otherwise ensure timer stopped.
        void ShowMessage(object sender, EventArgs e)
        {
            if (!EyeRest.Properties.Settings.Default.ShowMessage)
            {
                // ensure timer stopped
                try { myTimer.Stop(); } catch { }
                return;
            }

            // Show only a non-modal balloon tip; avoid blocking dialogs.
            try { notifyIcon.ShowBalloonTip(3000, "EyeRest reminder", "You should take rest for at least 20 seconds!", ToolTipIcon.Info); } catch { }
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
        private void CleanUpAndExit()
        {
            try { myTimer.Tick -= ShowMessage; } catch { }

            try { myTimer.Stop(); myTimer.Dispose(); } catch { }

            try { notifyIcon.Visible = false; notifyIcon.Dispose(); } catch { }

            // Exit the message loop for this ApplicationContext.
            ExitThread();
        }

        // Dispose managed resources when the context is disposed.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try { myTimer.Tick -= ShowMessage; } catch { }
                try { myTimer?.Dispose(); } catch { }
                try { notifyIcon?.Dispose(); } catch { }
                try { configWindow?.Dispose(); } catch { }
            }
            base.Dispose(disposing);
        }

    }
}