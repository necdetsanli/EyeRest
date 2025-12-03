namespace EyeRest
{
    partial class Configuration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuration));
            this.showMessageCheckBox = new System.Windows.Forms.CheckBox();
            this.intervalLabel = new System.Windows.Forms.Label();
            this.intervalNumeric = new System.Windows.Forms.NumericUpDown();
            this.useLeftClickCheckBox = new System.Windows.Forms.CheckBox();
            this.rememberSettingsCheckBox = new System.Windows.Forms.CheckBox();
            this.startWithWindowsCheckBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.intervalNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // showMessageCheckBox
            // 
            this.showMessageCheckBox.AutoSize = true;
            this.showMessageCheckBox.Checked = true;
            this.showMessageCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showMessageCheckBox.Location = new System.Drawing.Point(15, 15);
            this.showMessageCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.showMessageCheckBox.Name = "showMessageCheckBox";
            this.showMessageCheckBox.Size = new System.Drawing.Size(159, 24);
            this.showMessageCheckBox.TabIndex = 0;
            this.showMessageCheckBox.Text = "Enable reminders";
            this.showMessageCheckBox.UseVisualStyleBackColor = true;
            // 
            // intervalLabel
            // 
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.Location = new System.Drawing.Point(15, 50);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(135, 20);
            this.intervalLabel.TabIndex = 3;
            this.intervalLabel.Text = "Interval (minutes):";
            // 
            // intervalNumeric
            // 
            this.intervalNumeric.Location = new System.Drawing.Point(180, 48);
            this.intervalNumeric.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.intervalNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.intervalNumeric.Name = "intervalNumeric";
            this.intervalNumeric.Size = new System.Drawing.Size(80, 26);
            this.intervalNumeric.TabIndex = 4;
            this.intervalNumeric.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // useLeftClickCheckBox
            // 
            this.useLeftClickCheckBox.AutoSize = true;
            this.useLeftClickCheckBox.Location = new System.Drawing.Point(15, 86);
            this.useLeftClickCheckBox.Name = "useLeftClickCheckBox";
            this.useLeftClickCheckBox.Size = new System.Drawing.Size(228, 24);
            this.useLeftClickCheckBox.TabIndex = 5;
            this.useLeftClickCheckBox.Text = "Left-click toggles reminders";
            this.useLeftClickCheckBox.UseVisualStyleBackColor = true;
            // 
            // rememberSettingsCheckBox
            // 
            this.rememberSettingsCheckBox.AutoSize = true;
            this.rememberSettingsCheckBox.Location = new System.Drawing.Point(15, 120);
            this.rememberSettingsCheckBox.Name = "rememberSettingsCheckBox";
            this.rememberSettingsCheckBox.Size = new System.Drawing.Size(254, 24);
            this.rememberSettingsCheckBox.TabIndex = 6;
            this.rememberSettingsCheckBox.Text = "Remember my settings for future sessions";
            this.rememberSettingsCheckBox.UseVisualStyleBackColor = true;
            // 
            // startWithWindowsCheckBox
            // 
            this.startWithWindowsCheckBox.AutoSize = true;
            this.startWithWindowsCheckBox.Location = new System.Drawing.Point(15, 152);
            this.startWithWindowsCheckBox.Name = "startWithWindowsCheckBox";
            this.startWithWindowsCheckBox.Size = new System.Drawing.Size(222, 24);
            this.startWithWindowsCheckBox.TabIndex = 7;
            this.startWithWindowsCheckBox.Text = "Start automatically with Windows";
            this.startWithWindowsCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(388, 190);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(112, 35);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(272, 190);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 35);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 240);
            this.Controls.Add(this.startWithWindowsCheckBox);
            this.Controls.Add(this.rememberSettingsCheckBox);
            this.Controls.Add(this.useLeftClickCheckBox);
            this.Controls.Add(this.intervalNumeric);
            this.Controls.Add(this.intervalLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.showMessageCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Configuration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveSettings);
            this.Shown += new System.EventHandler(this.LoadSettings);
            ((System.ComponentModel.ISupportInitialize)(this.intervalNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox showMessageCheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label intervalLabel;
        private System.Windows.Forms.NumericUpDown intervalNumeric;
        private System.Windows.Forms.CheckBox useLeftClickCheckBox;
        private System.Windows.Forms.CheckBox rememberSettingsCheckBox;
        private System.Windows.Forms.CheckBox startWithWindowsCheckBox;
    }
}

