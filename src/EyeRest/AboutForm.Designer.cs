namespace EyeRest
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.LinkLabel linkGitHub;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblPrivacy;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.FlowLayoutPanel contentPanel;

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
            this.components = new System.ComponentModel.Container();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.linkGitHub = new System.Windows.Forms.LinkLabel();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblPrivacy = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // contentPanel
            // 
            this.contentPanel.AutoSize = true;
            this.contentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contentPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.contentPanel.WrapContents = false;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.contentPanel.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.TabIndex = 0;
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAppName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Text = "EyeRest";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Text = "Version";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.MaximumSize = new System.Drawing.Size(420, 0);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Text = "A lightweight Windows tray application that helps you follow the 20–20–20 rule for eye health.";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Text = "Developed by Necdet Þanlý";
            // 
            // linkGitHub
            // 
            this.linkGitHub.AutoSize = true;
            this.linkGitHub.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.linkGitHub.Name = "linkGitHub";
            this.linkGitHub.TabStop = true;
            this.linkGitHub.Text = "GitHub: necdetsanli/EyeRest";
            this.linkGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGitHub_LinkClicked);
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Text = "";
            // 
            // lblPrivacy
            // 
            this.lblPrivacy.AutoSize = true;
            this.lblPrivacy.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblPrivacy.Name = "lblPrivacy";
            this.lblPrivacy.Text = "EyeRest does not collect telemetry or personal data.";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 35);
            // Position: bottom-right with a small margin from form edges (16px right, 12px bottom)
            this.btnClose.Location = new System.Drawing.Point(480 - 16 - 112, 340 - 12 - 35);
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 340);
            // add controls into contentPanel
            this.contentPanel.Controls.Add(this.lblAppName);
            this.contentPanel.Controls.Add(this.lblVersion);
            this.contentPanel.Controls.Add(this.lblDescription);
            this.contentPanel.Controls.Add(this.lblAuthor);
            this.contentPanel.Controls.Add(this.linkGitHub);
            this.contentPanel.Controls.Add(this.lblCopyright);
            this.contentPanel.Controls.Add(this.lblPrivacy);

            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.contentPanel);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About EyeRest";
            this.AcceptButton = this.btnClose;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
