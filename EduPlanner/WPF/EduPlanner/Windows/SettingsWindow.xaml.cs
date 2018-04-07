using System;
using System.Windows;
using EduPlanner.Classes;

namespace EduPlanner.Windows {

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
            get => DataManager.Settings.ReceiveBetaUpdates;
            set => DataManager.Settings.ReceiveBetaUpdates = value;
        }

        public bool UseDemoContent {
            get => DataManager.Settings.UseDemoContent;
            set => DataManager.Settings.UseDemoContent = value;
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

            if (!DataManager.Authenticated) return;

            tgDriveSync.IsChecked = true;
            txtDriveSync.Text = String.Format("Your {0} data is currently being synced.", DataManager.APPLICATIONNAME);
        }
    }
}
