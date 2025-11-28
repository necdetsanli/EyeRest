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
        }

        // Populate controls from persisted settings when the dialog is shown.
        private void LoadSettings(object sender, EventArgs e)
        {
            // Reflect the current in-memory setting so the dialog shows the user's session choice.
            showMessageCheckBox.Checked = EyeRest.Properties.Settings.Default.ShowMessage;
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
                // Intentionally do not call Settings.Default.Save(); to keep default-on on app restart.
            }
        }
    }
}