using MaterialDesignThemes.Wpf;
using System;
using System.Deployment.Application;
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

        DispatcherTimer timer = new DispatcherTimer();

        int timerIntervalMin = 5;

        public MainWindow() {

            InitializeComponent();

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

        public void UpdateAgendaView() {
            AgendaView.Children.Clear();

            for (int i = 0; i < schedule.days.Count; i++) {
                if (schedule.days[i].hasClass) {
                    schedule.days[i].Order();

                    DayCard dayCard = new DayCard(schedule.days[i]);
                    StackPanel dayCardPanel = dayCard.FindName("ClassesView") as StackPanel;
                    AgendaView.Children.Add(dayCard);

                    for (int j = 0; j < schedule.days[i].classes.Count; j++) {
                        ClassCard card = new ClassCard(schedule.days[i].classes[j], schedule.days[i]);
                        dayCardPanel.Children.Add(card);
                    }
                }
            }

            UpdateHomeworkView();
        }

        public void UpdateHomeworkView() {
            HomeworkView.Children.Clear();

            for (int i = 0; i < DataManager.schedule.classes.Count; i++) {
                ClassHomeworkCard classCard = new ClassHomeworkCard(DataManager.schedule.classes[i]);
                HomeworkView.Children.Add(classCard);

                if (classCard._class.homeworks.Count > 0) {
                    for (int j = 0; j < classCard._class.homeworks.Count; j++) {
                        StackPanel panel = classCard.FindName("classHomework") as StackPanel;
                        HomeworkCard homework = new HomeworkCard(classCard._class, classCard._class.homeworks[j]);
                        panel.Children.Add(homework);
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
        }

        /// <summary>
        /// Change to Assignments View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnViewAssignments_Click(object sender, RoutedEventArgs e) {
            AgendaView.Visibility = Visibility.Collapsed;
            HomeworkView.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Refresh the view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Refresh(object sender, EventArgs e) {
            DateTime currentDateTime = DateTime.Now;

            for (int i = 0; i < AgendaView.Children.Count; i++) {

                DayCard dayCard = AgendaView.Children[i] as DayCard;
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

        private void BtnAddClass_Click(object sender, RoutedEventArgs e) {
            AddClassWindow addClass = new AddClassWindow();
            addClass.Closed += new EventHandler(WindowAddEditClass_Closed);
            addClass.ShowDialog();
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
