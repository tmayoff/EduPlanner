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
            get { return DataManager.settings.checkForUpdatesOnStartup; }
            set { DataManager.settings.checkForUpdatesOnStartup = value; }
        }
        public bool MinimizeToTray {
            get { return DataManager.settings.minimizeToTray; }
            set { DataManager.settings.minimizeToTray = value; }
        }

        public bool UseBetaVersion {
            get { return DataManager.settings.receiveBetaUpdates; }
            set { DataManager.settings.receiveBetaUpdates = value; }
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
            UseBetaVersion = DataManager.settings.receiveBetaUpdates;
            CheckForUpdatesOnStartUp = DataManager.settings.checkForUpdatesOnStartup;
            MinimizeToTray = DataManager.settings.minimizeToTray;
            UseDemoContent = DataManager.settings.useDemoContent;
        }
    }
}
