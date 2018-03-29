using MaterialDesignThemes.Wpf;
using System;
using System.Deployment.Application;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using EduPlanner.Windows;

namespace EduPlanner {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Schedule schedule;
        Data data;
        DateTime upcomingTime;

        DispatcherTimer timer = new DispatcherTimer();
        NotifyIcon notify;

        int timerIntervalMin = 5;

        bool viewingAgenda;

        public MainWindow() {

            InitializeComponent();
<<<<<<< HEAD
            Updater.CheckForUpdate(true);
=======

            UpdateChecker.CheckForUpdate();

            //Initialize things
            notify = new NotifyIcon {
                Icon = new System.Drawing.Icon(@"../../icon.ico"),
                Text = "EduPlanner",
                Visible = true
            };

            notify.DoubleClick += Notify_DoubleClick;
>>>>>>> 8220a5654b7aa9f4c3088a57c7325b1f7d55ca06

            upcomingTime = DateTime.Now + new TimeSpan(7, 0, 0, 0);

            //Load / Create a schedule
            data = new Data();
            data.Load();

            schedule = DataManager.schedule;
            DataManager.mainWindow = this;
            UpdateAgendaView();
            UpdateHomeworkView();

            //Timer
            Refresh(this, EventArgs.Empty);
            timer.Tick += new EventHandler(Refresh);
            timer.Interval = new TimeSpan(0, timerIntervalMin, 0);

        }

        private void Notify_DoubleClick(object sender, EventArgs e) {
            Show();
            WindowState = WindowState.Normal;
            notify.Visible = false;
        }

        public void UpdateAgendaView() {
            Agenda.Children.Clear();

            for (int i = 0; i < schedule.days.Count; i++) {
                if (schedule.days[i].hasClass) {
                    schedule.days[i].Order();

                    DayCard dayCard = new DayCard(schedule.days[i]);
                    StackPanel dayCardPanel = dayCard.FindName("ClassesView") as StackPanel;
                    Agenda.Children.Add(dayCard);

                    for (int j = 0; j < schedule.days[i].classes.Count; j++) {
                        ClassCard card = new ClassCard(schedule.days[i].classes[j], schedule.days[i]);
                        dayCardPanel.Children.Add(card);
                    }
                }
            }

            UpdateHomeworkView();
        }

        public void UpdateHomeworkView() {
            Homework.Children.Clear();
            Upcoming.Children.Clear();

            ClassHomeworkCard currentCard;
            StackPanel currentCardPanel;

            Homework homework;
            HomeworkCard homeworkCard;

            //Main view
            for (int i = 0; i < DataManager.schedule.classes.Count; i++) {
                currentCard = new ClassHomeworkCard(DataManager.schedule.classes[i]);
                Homework.Children.Add(currentCard);

                if (currentCard._class.homeworks.Count > 0) {
                    for (int j = 0; j < currentCard._class.homeworks.Count; j++) {
                        homework = currentCard._class.homeworks[j];

                        currentCardPanel = currentCard.FindName("classHomework") as StackPanel;
                        homeworkCard = new HomeworkCard(currentCard._class, homework, false);
                        currentCardPanel.Children.Add(homeworkCard);

                        if (homework.dueDate <= upcomingTime) {
                            homeworkCard = new HomeworkCard(currentCard._class, homework, true);
                            Upcoming.Children.Add(homeworkCard);
                        }
                    }
                }
            }
        }

        #region Button Handlers

        /// <summary>
        /// Change to Agenda View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnViewAgenda_Click(object sender, RoutedEventArgs e) {
            AgendaView.Visibility = Visibility.Visible;
            HomeworkView.Visibility = Visibility.Collapsed;
            viewingAgenda = true;
        }

        /// <summary>
        /// Change to Assignments View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnViewAssignments_Click(object sender, RoutedEventArgs e) {
            AgendaView.Visibility = Visibility.Collapsed;
            HomeworkView.Visibility = Visibility.Visible;
            viewingAgenda = false;
        }

        /// <summary>
        /// Refresh the view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Refresh(object sender, EventArgs e) {
            DateTime currentDateTime = DateTime.Now;

            for (int i = 0; i < Agenda.Children.Count; i++) {

                DayCard dayCard = Agenda.Children[i] as DayCard;
                Day day = dayCard.day;
                Grid dayCardCard = dayCard.FindName("Card") as Grid;

                if (day.day == DateTime.Today.DayOfWeek) {
                    dayCardCard.Background = Brushes.LightBlue;

                    StackPanel classesPanel = dayCard.FindName("ClassesView") as StackPanel;

                    for (int j = 0; j < classesPanel.Children.Count; j++) {

                        ClassCard classCard = classesPanel.Children[j] as ClassCard;
                        Class _class = classCard._class;
                        Card classCardCard = classCard.FindName("Card") as Card;
                        DateTime start = _class.classTimes[day.day][0].Value;
                        DateTime end = _class.classTimes[day.day][1].Value;

                        if (currentDateTime.TimeOfDay >= start.TimeOfDay && currentDateTime.TimeOfDay <= end.TimeOfDay) {
                            classCardCard.Background = Brushes.LightGreen;
                            txtCurrentClass.Text = "Current Class: " + _class.className;
                        } else {
                            classCardCard.Background = Brushes.White;
                            txtCurrentClass.Text = "";
                        }
                    }
                } else
                    dayCardCard.Background = Brushes.White;
            }
        }

        private void BtnViewAll_Click(object sender, RoutedEventArgs e) {
            ViewAllAssignments view = new ViewAllAssignments();
            view.ShowDialog();
        }

        private void BtnRefreshList_Click(object sender, RoutedEventArgs e) {
            Refresh(sender, EventArgs.Empty);
        }

        private void BtnClassList_Click(object sender, RoutedEventArgs e) {
            ClassList classList = new ClassList();
            classList.Show();
        }

<<<<<<< HEAD
        private void BtnCheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            Updater.CheckForUpdate();
=======
        private void BtnCheckForUpdates_Click(object sender, RoutedEventArgs e) {
            UpdateChecker.CheckForUpdate();
>>>>>>> 8220a5654b7aa9f4c3088a57c7325b1f7d55ca06
        }

        private void BtnAddClass_Click(object sender, RoutedEventArgs e) {
            AddClassWindow addClass = new AddClassWindow();
            addClass.Closed += new EventHandler(WindowAddEditClass_Closed);
            addClass.ShowDialog();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            if (viewingAgenda) {
                AddClassWindow addClass = new AddClassWindow();
                addClass.Closed += new EventHandler(WindowAddEditClass_Closed);
                addClass.ShowDialog();
            } else {
                AddHomeworkWindow addHomework = new AddHomeworkWindow();
                addHomework.Closed += new EventHandler(WindowAddEditHomework_Closed);
                addHomework.ShowDialog();
            }
        }

        private void BtnSave(object sender, RoutedEventArgs e) {
            data.Save();
        }

        private void BtnLoad(object sender, RoutedEventArgs e) {
            data.Load();
            UpdateAgendaView();
        }

        #endregion

        #region Window Event Handlers
        private void Window_StateChanged(object sender, EventArgs e) {
            if (sender is Window) {
                Window win = sender as Window;
                if (win.WindowState == WindowState.Minimized) {
                    Hide();
                    notify.Visible = true;
                } else {
                    Show();
                    notify.Visible = false;
                }
            }
        }

        public void WindowAddEditClass_Closed(object sender, EventArgs e) {
            UpdateAgendaView();
        }

        public void WindowAddEditHomework_Closed(object sender, EventArgs e) {
            UpdateHomeworkView();
        }

        private void Window_Closed(object sender, EventArgs e) {
            BtnSave(sender, new RoutedEventArgs());
        }

        #endregion
    }
}
