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

        private bool _viewingAgenda = true;

        public MainWindow() {
            DataManager.Authenticated = DataManager.GoogleAuthenticate();

            InitializeComponent();

            //Load All Data Settings and data
            _data = new Data();

            //Initialize things
            _upcomingTime = DateTime.Now + new TimeSpan(7, 0, 0, 0);

            if (DataManager.Settings.checkForUpdatesOnStartup)
                Updater.CheckForUpdate(true);

            _schedule = DataManager.Schedule;
            DataManager.MainWindow = this;
            UpdateAgendaView();
            UpdateHomeworkView();

            //Timer
            _timer.Tick += RefreshEvent;
            _timer.Interval = new TimeSpan(0, TIMERINTERVALMIN, 0);
        }

        public void UpdateClassListView() {
            classList.Children.Clear();

            foreach (Class _class in DataManager.Schedule.classes) {
                ClassCard classCard = new ClassCard(_class);
                classList.Children.Add(classCard);
            }
        }

        /// <summary>
        /// Updates the class Schedule view
        /// </summary>
        public void UpdateAgendaView() {
            panelAgenda.Children.Clear();

            foreach (Day day in _schedule.days) {
                if (!day.hasClass)
                    continue;

                DayCard dayCard = new DayCard(day);
                if (!(dayCard.FindName("ClassesView") is StackPanel dayCardPanel))
                    continue;

                panelAgenda.Children.Add(dayCard);

                foreach (Class _class in day.classes) {
                    ClassCard card = new ClassCard(_class, day);

                    dayCardPanel.Children.Add(card);
                }
            }

            Refresh();
            UpdateHomeworkView();
        }

        /// <summary>
        /// Updates the homeworkPanel views
        /// </summary>
        public void UpdateHomeworkView() {
            homeworkPanel.Children.Clear();
            upcoming.Children.Clear();

            foreach (Class _class in DataManager.Schedule.classes) {

                if (_class.Homeworks.Count == 0)
                    continue;

                ClassHomeworkCard currentCard = new ClassHomeworkCard(_class);
                homeworkPanel.Children.Add(currentCard);

                foreach (Homework homework in _class.Homeworks) {

                    if (!(currentCard.FindName("classHomework") is StackPanel currentCardPanel))
                        continue;

                    HomeworkCard homeworkCard = new HomeworkCard(currentCard._class, homework, false);
                    currentCardPanel.Children.Add(homeworkCard);

                    if (homework.dueDate > _upcomingTime)
                        continue;

                    homeworkCard = new HomeworkCard(currentCard._class, homework, true);
                    upcoming.Children.Add(homeworkCard);
                }
            }
        }

        /// <summary>
        /// Refresh the view
        /// </summary>
        public void Refresh() {
            DateTime currentDateTime = DateTime.Now;

            _currentClass = null;

            //Go through each day in the panelAgenda view
            for (int i = 0; i < panelAgenda.Children.Count; i++) {

                if (!(panelAgenda.Children[i] is DayCard dayCard))
                    continue;

                Day day = dayCard.day;
                if (!(dayCard.FindName("card") is Grid dayCardCard))
                    continue;

                //If the day we're looking at is today
                if (day.day == DateTime.Today.DayOfWeek) {

                    dayCardCard.Background = Brushes.LightBlue;

                    if (!(dayCard.FindName("ClassesView") is StackPanel classesPanel))
                        continue;

                    //Loop through all the classes in that day
                    for (int j = 0; j < classesPanel.Children.Count; j++) {

                        if (!(classesPanel.Children[j] is ClassCard classCard))
                            continue;

                        Class _class = classCard._class;
                        if (!(classCard.FindName("card") is Card classCardCard))
                            continue;

                        DateTime start = _class.classTimes[(int)day.day][0].Value;
                        DateTime end = _class.classTimes[(int)day.day][1].Value;

                        //If the current class is we're looking at is right now
                        if (currentDateTime.TimeOfDay >= start.TimeOfDay && currentDateTime.TimeOfDay <= end.TimeOfDay) {
                            classCardCard.Background = Brushes.LightGreen;
                            txtCurrentClass.Text = "Current Class: " + _class.className;
                            _currentClass = _class;
                        } else {
                            classCardCard.Background = Brushes.White;

                            //Get next class
                            if (j < classesPanel.Children.Count - 1) {
                                classCard = classesPanel.Children[j + 1] as ClassCard;
                                _class = classCard._class;

                                _currentClass = _class;
                                txtCurrentClass.Text = "upcoming: " + _currentClass.className;
                            } else {
                                _currentClass = null;
                                txtCurrentClass.Text = "";
                            }
                        }
                    }
                } else
                    dayCardCard.Background = Brushes.White;
            }
        }

        public void ChangeView(Grid newView) {
            for (int i = 0; i < contentViews.Children.Count; i++) {
                if (!(contentViews.Children[i] is Grid grid))
                    continue;

                grid.Visibility = grid.Name == newView.Name ? Visibility.Visible : Visibility.Collapsed;
            }

            UpdateAgendaView();
            UpdateClassListView();
            UpdateHomeworkView();
        }

        #region Button Handlers

        public void BtnImportData_Click(object sender, RoutedEventArgs e) {
            Data.Import();

            UpdateAgendaView();
            UpdateClassListView();
            UpdateHomeworkView();
            Refresh();
        }

        public void BtnExportData_Click(object sender, RoutedEventArgs e) {
            Data.Export();
        }

        public void BtnClassList_Click(object sender, RoutedEventArgs e) {
            ChangeView(classListView);
        }

        /// <summary>
        /// Change to panelAgenda View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnViewAgenda_Click(object sender, RoutedEventArgs e) {
            ChangeView(agendaView);
            _viewingAgenda = true;
        }

        /// <summary>
        /// Change to Assignments View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnViewAssignments_Click(object sender, RoutedEventArgs e) {
            ChangeView(homeworkView);
            _viewingAgenda = false;
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

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            if (_viewingAgenda) {
                AddClassWindow addClass = new AddClassWindow();
                addClass.Closed += WindowAddEditClass_Closed;
                addClass.ShowDialog();
            } else {
                AddHomeworkWindow addHomework = new AddHomeworkWindow();
                addHomework.Closed += WindowAddEditHomework_Closed;
                addHomework.ShowDialog();
            }
        }

        #endregion

        #region Window Event Handlers

        private void RefreshEvent(object sender, EventArgs e) {
            Refresh();
        }

        public void WindowAddEditClass_Closed(object sender, EventArgs e) {
            UpdateAgendaView();
        }

        public void WindowAddEditHomework_Closed(object sender, EventArgs e) {
            UpdateHomeworkView();
        }

        private void Window_Closed(object sender, EventArgs e) {
            _data.Save();
        }

        #endregion
    }
}
