using EduPlanner.Classes;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace EduPlanner
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            Title = "About " + DataManager.APPLICATIONNAME;
            txtAppName.Text = DataManager.APPLICATIONNAME;
            txtVersion.Text = "Version " + Updater.curVersion;
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
