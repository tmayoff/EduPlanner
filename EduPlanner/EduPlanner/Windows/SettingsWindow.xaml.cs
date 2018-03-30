using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EduPlanner.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (Settings.checkForUpdatesOnStartup == true)
            {
                tgCheckForUpdatesOnStartup.IsChecked = true;
            }
            if (Settings.minimizeToTray == true)
            {
                tgMinimizeToTray.IsChecked = true;
            }
            if (Settings.receiveBetaUpdates == true)
            {
                tgReceiveBetaUpdates.IsChecked = true;
            }
        }

        private void tgCheckForUpdatesOnStartup_Checked(object sender, RoutedEventArgs e)
        {
            Settings.checkForUpdatesOnStartup = true;
        }

        private void tgCheckForUpdatesOnStartup_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.checkForUpdatesOnStartup = false;
        }

        private void tgMinimizeToTray_Checked(object sender, RoutedEventArgs e)
        {
            Settings.minimizeToTray = true;
        }

        private void tgMinimizeToTray_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.minimizeToTray = false;
        }

        private void tgReceiveBetaUpdates_Checked(object sender, RoutedEventArgs e)
        {
            Settings.receiveBetaUpdates = true;
        }

        private void tgReceiveBetaUpdates_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.receiveBetaUpdates = false;
        }
    }
}
