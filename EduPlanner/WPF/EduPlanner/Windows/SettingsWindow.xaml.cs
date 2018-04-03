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

namespace EduPlanner.Windows {
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window {

        public bool CheckForUpdatesOnStartUp {
            get { return DataManager.Settings.checkForUpdatesOnStartup; }
            set { DataManager.Settings.checkForUpdatesOnStartup = value; }
        }
        public bool MinimizeToTray {
            get { return DataManager.Settings.minimizeToTray; }
            set { DataManager.Settings.minimizeToTray = value; }
        }

        public bool UseBetaVersion {
            get { return DataManager.Settings.receiveBetaUpdates; }
            set { DataManager.Settings.receiveBetaUpdates = value; }
        }

        public bool UseDemoContent
        {
            get { return DataManager.settings.useDemoContent; }
            set { DataManager.settings.useDemoContent = value; }
        }

        public SettingsWindow() {
            InitializeComponent();

            DataContext = this;
            LoadSettings();
        }

        private void LoadSettings() {
<<<<<<< HEAD:EduPlanner/EduPlanner/Windows/SettingsWindow.xaml.cs
            UseBetaVersion = DataManager.settings.receiveBetaUpdates;
            CheckForUpdatesOnStartUp = DataManager.settings.checkForUpdatesOnStartup;
            MinimizeToTray = DataManager.settings.minimizeToTray;
            UseDemoContent = DataManager.settings.useDemoContent;
=======
            UseBetaVersion = DataManager.Settings.receiveBetaUpdates;
            CheckForUpdatesOnStartUp = DataManager.Settings.checkForUpdatesOnStartup;
            MinimizeToTray = DataManager.Settings.minimizeToTray;
>>>>>>> 4311f7d6517501f8c293d4bae86a1474d761e220:EduPlanner/WPF/EduPlanner/Windows/SettingsWindow.xaml.cs
        }
    }
}
