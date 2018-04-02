using MaterialDesignThemes.Wpf;
using System;
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

        private readonly Schedule _schedule;
        private readonly Data _data;
        private readonly DateTime _upcomingTime;

        Class currentClass;

        DispatcherTimer timer = new DispatcherTimer();

        private const int TIMERINTERVALMIN = 5;

        private bool _viewingAgenda = true;

        public MainWindow() {
            DataManager.Authenticated = DataManager.GoogleAuthenticate();

            InitializeComponent();

            //Load Settings and data
            _data = new Data();

            //Initialize things

            _upcomingTime = DateTime.Now + new TimeSpan(7, 0, 0, 0);

            Updater.CheckForUpdate(DataManager.Settings.checkForUpdatesOnStartup);

            _schedule = DataManager.Schedule;
            DataManager.MainWindow = this;
            UpdateAgendaView();
            UpdateHomeworkView();

            //Timer
            timer.Tick += RefreshEvent;
            timer.Interval = new TimeSpan(0, TIMERINTERVALMIN, 0);
        }

        /// <summary>
        /// Updates the class Schedule view
        /// </summary>
        public void UpdateAgendaView() {
            Agenda.Children.Clear();

            for (int i = 0; i < _schedule.days.Count; i++) {
                if (_schedule.days[i].hasClass) {
                    _schedule.days[i].Order();

                    DayCard dayCard = new DayCard(_schedule.days[i]);
                    StackPanel dayCardPanel = dayCard.FindName("ClassesView") as StackPanel;
                    Agenda.Children.Add(dayCard);

                    for (int j = 0; j < _schedule.days[i].classes.Count; j++) {
                        ClassCard card = new ClassCard(_schedule.days[i].classes[j], _schedule.days[i]);
                        dayCardPanel.Children.Add(card);
                    }
                }
            }

            Refresh();
            UpdateHomeworkView();
        }

        /// <summary>
        /// Updates the homework views
        /// </summary>
        public void UpdateHomeworkView() {
            Homework.Children.Clear();
            Upcoming.Children.Clear();

            ClassHomeworkCard currentCard;
            StackPanel currentCardPanel;

            Homework homework;
            HomeworkCard homeworkCard;

            for (int i = 0; i < DataManager.Schedule.classes.Count; i++) {


                if (DataManager.Schedule.classes[i].homeworks.Count > 0) {
                    currentCard = new ClassHomeworkCard(DataManager.Schedule.classes[i]);
                    Homework.Children.Add(currentCard);

                    for (int j = 0; j < currentCard._class.homeworks.Count; j++) {
                        homework = currentCard._class.homeworks[j];

                        currentCardPanel = currentCard.FindName("classHomework") as StackPanel;
                        homeworkCard = new HomeworkCard(currentCard._class, homework, false);
                        currentCardPanel.Children.Add(homeworkCard);

                        if (homework.dueDate <= _upcomingTime) {
                            homeworkCard = new HomeworkCard(currentCard._class, homework, true);
                            Upcoming.Children.Add(homeworkCard);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Refresh the view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Refresh() {
            DateTime currentDateTime = DateTime.Now;

            currentClass = null;

            DayCard dayCard;
            Day day;
            Grid dayCardCard;

            StackPanel classesPanel;
            ClassCard classCard;
            Class _class;
            Card classCardCard;
            DateTime start;
            DateTime end;

            //Go through each day in the agenda view
            for (int i = 0; i < Agenda.Children.Count; i++) {

                dayCard = Agenda.Children[i] as DayCard;
                day = dayCard.day;
                dayCardCard = dayCard.FindName("Card") as Grid;

                //If the day we're looking at is today
                if (day.day == DateTime.Today.DayOfWeek) {

                    dayCardCard.Background = Brushes.LightBlue;
                    classesPanel = dayCard.FindName("ClassesView") as StackPanel;

                    //Loop through all the classes in that day
                    for (int j = 0; j < classesPanel.Children.Count; j++) {

                        classCard = classesPanel.Children[j] as ClassCard;
                        _class = classCard._class;
                        classCardCard = classCard.FindName("Card") as Card;
                        start = _class.classTimes[day.day][0].Value;
                        end = _class.classTimes[day.day][1].Value;

                        //If the current class is we're looking at is right now
                        if (currentDateTime.TimeOfDay >= start.TimeOfDay && currentDateTime.TimeOfDay <= end.TimeOfDay) {
                            classCardCard.Background = Brushes.LightGreen;
                            txtCurrentClass.Text = "Current Class: " + _class.className;
                            currentClass = _class;
                        } else {
                            classCardCard.Background = Brushes.White;

                            //Get next class
                            if (j < classesPanel.Children.Count - 1) {
                                classCard = classesPanel.Children[j + 1] as ClassCard;
                                _class = classCard._class;

                                currentClass = _class;
                                txtCurrentClass.Text = "Upcoming: " + currentClass.className;
                            } else {
                                currentClass = null;
                                txtCurrentClass.Text = "";
                            }
                        }
                    }
                } else
                    dayCardCard.Background = Brushes.White;
            }
        }

        #region Button Handlers

        public void BtnExportData_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Change to Agenda View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnViewAgenda_Click(object sender, RoutedEventArgs e) {
            AgendaView.Visibility = Visibility.Visible;
            HomeworkView.Visibility = Visibility.Collapsed;
            _viewingAgenda = true;
        }

        /// <summary>
        /// Change to Assignments View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnViewAssignments_Click(object sender, RoutedEventArgs e) {
            AgendaView.Visibility = Visibility.Collapsed;
            HomeworkView.Visibility = Visibility.Visible;
            _viewingAgenda = false;
        }

        private void BtnViewAll_Click(object sender, RoutedEventArgs e) {
            ViewAllAssignments view = new ViewAllAssignments();
            view.ShowDialog();
        }

        private void BtnRefreshList_Click(object sender, RoutedEventArgs e) {
            Refresh();
        }

        private void BtnClassList_Click(object sender, RoutedEventArgs e) {
            ClassList classList = new ClassList();
            classList.Show();
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
