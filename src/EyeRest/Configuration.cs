using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EyeRest
{
    public partial class Configuration : Form
    {
        public Configuration()
        {
            // Initialize form controls and event handlers
            InitializeComponent();
            this.Icon = EyeRest.Properties.Resources.AppIcon;
        }

        // Populate controls from persisted settings when the dialog is shown.
        private void LoadSettings(object sender, EventArgs e)
        {
            // Reflect the current in-memory setting so the dialog shows the user's session choice.
            showMessageCheckBox.Checked = EyeRest.Properties.Settings.Default.ShowMessage;

            // Load the session-only interval from the application context static field.
            try
            {
                intervalNumeric.Value = Math.Min(intervalNumeric.Maximum, Math.Max(intervalNumeric.Minimum, EyeRestApplicationContext.ReminderIntervalMinutes));
            }
            catch
            {
                intervalNumeric.Value = 20;
            }

            try
            {
                useLeftClickCheckBox.Checked = EyeRestApplicationContext.UseLeftClickToggle;
            }
            catch
            {
                useLeftClickCheckBox.Checked = false;
            }
        }

        // Save settings when the user confirms (DialogResult.OK).
        private void SaveSettings(object sender, FormClosingEventArgs e)
        {
            // If the user clicked "Save"
            if (this.DialogResult == DialogResult.OK)
            {
                // Update in-memory setting so change takes effect this session.
                // Do not call Save() to avoid persisting the user's choice across restarts.
                EyeRest.Properties.Settings.Default.ShowMessage = showMessageCheckBox.Checked;

                try
                {
                    EyeRestApplicationContext.ReminderIntervalMinutes = (int)intervalNumeric.Value;
                }
                catch
                {
                    EyeRestApplicationContext.ReminderIntervalMinutes = 20;
                }

                try
                {
                    EyeRestApplicationContext.UseLeftClickToggle = useLeftClickCheckBox.Checked;
                }
                catch
                {
                    EyeRestApplicationContext.UseLeftClickToggle = false;
                }

                // Intentionally do not call Settings.Default.Save(); to keep defaults on app restart.
            }
        }
    }
}