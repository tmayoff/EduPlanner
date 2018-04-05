using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using EduPlanner.Windows;

namespace EduPlanner {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private const int TIMERINTERVALMIN = 5;

        private readonly Schedule _schedule;
        private readonly Data _data;
        private readonly DateTime _upcomingTime;

        private readonly DispatcherTimer _timer = new DispatcherTimer();

        private Class _currentClass;

        public MainWindow() {
            DataManager.Authenticated = DataManager.GoogleAuthenticate();

            InitializeComponent();
            DataContext = this;

            //Load All Data Settings and data
            _data = new Data();

            //Initialize things
            _upcomingTime = DateTime.Now + new TimeSpan(7, 0, 0, 0);

            Updater.CheckForUpdate(true);

            _schedule = DataManager.Schedule;
            DataManager.MainWindow = this;
            ChangeView(todayView);

            //Timer
            _timer.Tick += RefreshEvent;
            _timer.Interval = new TimeSpan(0, TIMERINTERVALMIN, 0);
        }

        #region Button Handlers

        private void BtnImportData_Click(object sender, RoutedEventArgs e) {
            Data.Import();

            UpdateAgendaView();
            UpdateClassListView();
            UpdateHomeworkView();
            Refresh();
        }

        private void BtnExportData_Click(object sender, RoutedEventArgs e) {
            Data.Export();
        }

        private void BtnCheckForUpdates_Click(object sender, RoutedEventArgs e) {
            Updater.CheckForUpdate();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e) {
            //About about = new About();
            //about.Show();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e) {
            SettingsWindow settings = new SettingsWindow();
            settings.Show();
        }

        private void BtnAddClass_Click(object sender, RoutedEventArgs e) {
            AddClassWindow addClass = new AddClassWindow();
            addClass.Closed += WindowAddEditClass_Closed;
            addClass.ShowDialog();
        }

        private void BtnAddAssignment_Click(object sender, RoutedEventArgs e) {
            AddHomeworkWindow addHomework = new AddHomeworkWindow();
            addHomework.Closed += WindowAddEditHomework_Closed;
            addHomework.ShowDialog();
        }

        #endregion

        #region Window Event Handlers

        public void RefreshEvent(object sender, EventArgs e) {
            Refresh();
        }

        public void WindowAddEditClass_Closed(object sender, EventArgs e) {
            UpdateAgendaView();
            UpdateTodayView();
            UpdateClassListView();
        }

        public void WindowAddEditHomework_Closed(object sender, EventArgs e) {
            UpdateHomeworkView();
        }

        public void Window_Closed(object sender, EventArgs e) {
            _data.Save();
        }

        #endregion
    }
}
