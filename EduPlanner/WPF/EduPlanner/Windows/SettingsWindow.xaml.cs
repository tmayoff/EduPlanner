using System.Windows;

namespace EduPlanner.Windows {
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window {

        public bool CheckForUpdatesOnStartUp {
            get => DataManager.Settings.CheckForUpdatesOnStartup;
            set => DataManager.Settings.CheckForUpdatesOnStartup = value;
        }

        public bool MinimizeToTray {
            get => DataManager.Settings.MinimizeToTray;
            set => DataManager.Settings.MinimizeToTray = value;
        }

        public bool UseBetaVersion {
            get { return DataManager.Settings.ReceiveBetaUpdates; }
            set { DataManager.Settings.ReceiveBetaUpdates = value; }
        }

        public bool UseDemoContent {
            get { return DataManager.Settings.UseDemoContent; }
            set { DataManager.Settings.UseDemoContent = value; }
        }

        public SettingsWindow() {
            InitializeComponent();

            DataContext = this;
            LoadSettings();
        }

        private void LoadSettings() {
            UseBetaVersion = DataManager.Settings.ReceiveBetaUpdates;
            CheckForUpdatesOnStartUp = DataManager.Settings.CheckForUpdatesOnStartup;
            MinimizeToTray = DataManager.Settings.MinimizeToTray;
            UseDemoContent = DataManager.Settings.UseDemoContent;
            UseBetaVersion = DataManager.Settings.ReceiveBetaUpdates;
            CheckForUpdatesOnStartUp = DataManager.Settings.CheckForUpdatesOnStartup;
            MinimizeToTray = DataManager.Settings.MinimizeToTray;
        }
    }
}
