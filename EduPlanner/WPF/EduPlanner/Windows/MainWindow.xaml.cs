using System;
using System.Windows;
using System.Windows.Threading;
using EduPlanner.Windows;

//using EduPlanner.Windows;

namespace EduPlanner {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private const int TIMERINTERVALMIN = 5;

        private readonly Data _data;
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow() {

            InitializeComponent();

            DataManager.Authenticated = DataManager.GoogleAuthenticate();

            DataContext = this;

            //Load All Data Settings and data
            _data = new Data();

            //Initialize things
            Updater.CheckForUpdate(true);

            DataManager.MainWindow = this;
            ChangeView(todayView);

            //Timer
            _timer.Tick += RefreshEvent;
            _timer.Interval = new TimeSpan(0, TIMERINTERVALMIN, 0);
        }

        #region Button Handlers

        private void BtnImportData_Click(object sender, RoutedEventArgs e) {
            Data.Import();

            //UpdateAgendaView();
            //UpdateClassListView();
            //UpdateView();
            //Refresh();
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
            Windows.AddAssignmentWindow addAssignment = new Windows.AddAssignmentWindow();
            addAssignment.Closed += WindowAddEditHomework_Closed;
            addAssignment.ShowDialog();
        }

        #endregion

        #region Window Event Handlers

        public void RefreshEvent(object sender, EventArgs e) {
            //Refresh();
        }

        public void WindowAddEditClass_Closed(object sender, EventArgs e) {
            UpdateViews();
        }

        public void WindowAddEditHomework_Closed(object sender, EventArgs e) {
            UpdateViews();
        }

        public void Window_Closed(object sender, EventArgs e) {
            _data.Save();
        }

        #endregion
    }
}
