using System;
using System.Reflection;
using System.Windows.Forms;

namespace EyeRest
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.AppIcon;
            InitializeMetadata();
        }

        private void InitializeMetadata()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string infoVersion = null;

                try
                {
                    var attr = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
                    if (attr != null && !string.IsNullOrEmpty(attr.InformationalVersion))
                        infoVersion = attr.InformationalVersion;
                }
                catch { }

                if (string.IsNullOrEmpty(infoVersion))
                {
                    try { infoVersion = assembly.GetName().Version.ToString(); } catch { infoVersion = ""; }
                }

                if (!string.IsNullOrEmpty(infoVersion))
                    lblVersion.Text = "Version " + infoVersion;

                try
                {
                    var copyAttr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
                    if (copyAttr != null && !string.IsNullOrEmpty(copyAttr.Copyright))
                        lblCopyright.Text = copyAttr.Copyright;
                }
                catch { }

                // Configure GitHub link
                linkGitHub.Links.Clear();
                linkGitHub.Links.Add(0, linkGitHub.Text.Length, "https://github.com/necdetsanli/EyeRest");
            }
            catch
            {
                // ignore metadata failures
            }
        }

        private void linkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                var target = e.Link.LinkData as string;
                if (!string.IsNullOrEmpty(target))
                {
                    System.Diagnostics.Process.Start(target);
                }
            }
            catch { }
        }
    }
}
