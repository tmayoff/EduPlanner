using EduPlanner.Classes;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace EduPlanner
{
    public partial class About : Window
    {
        public static Version currentVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString(3));// = string.Format("{0} ({1})", Assembly.GetExecutingAssembly().GetName().Version.ToString(2), Assembly.GetExecutingAssembly().GetName().Version.Build);

        public About()
        {
            InitializeComponent();

            Title = "About " + DataManager.APPLICATIONNAME;
            txtAppName.Text = DataManager.APPLICATIONNAME;

            string[] currentBuild = currentVersion.ToString().Split('.');
            if (currentBuild[2] == "0")
            {
                txtVersion.Text = "Version " + currentVersion.ToString(2);
            }
            else
            {
                txtVersion.Text = string.Format("Version {0} ({1})", currentVersion.ToString(2), currentBuild[2]);
            }


            hlWebsite.TextDecorations = null;
            txtCopyright.Text = string.Format("© {0} Tyler Mayoff & Joseph Di Pasquale. All Rights Reserved.", DateTime.Now.Year);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void hlWebsite_MouseEnter(object sender, EventArgs e)
        {
            hlWebsite.TextDecorations = TextDecorations.Underline;
        }

        private void hlWebsite_MouseLeave(object sender, EventArgs e)
        {
            hlWebsite.TextDecorations = null;
        }
    }
}
