using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
        }

        #region Event Handlers

        public void Refresh(object sender, EventArgs e) {
            DateTime currentDateTime = DateTime.Today;

            for (int i = 0; i < AgendaView.Children.Count; i++) {

                DayCard dayCard = AgendaView.Children[i] as DayCard;
                Day day = dayCard.day;
                Card dayCardCard = dayCard.FindName("Card") as Card;

                if (day.day == DateTime.Today.DayOfWeek) {
                    dayCardCard.Background = Brushes.LightBlue;

                    StackPanel classesPanel = dayCard.FindName("ClassesView") as StackPanel;

                    for (int j = 0; j < classesPanel.Children.Count; j++) {

                        ClassCard classCard = classesPanel.Children[j] as ClassCard;
                        Class _class = classCard._class;
                        Card classCardCard = classCard.FindName("Card") as Card;
                        DateTime start = _class.classTimes[day.day][0].Value;
                        DateTime end = _class.classTimes[day.day][1].Value;

                        if (currentDateTime >= start && currentDateTime <= end) {
                            classCardCard.Background = Brushes.LightGreen;
                            txtCurrentClass.Text = _class.className;
                        } else {
                            classCardCard.Background = Brushes.White;
                            txtCurrentClass.Text = "";
                        }
                    }
                } else
                    dayCardCard.Background = Brushes.White;
            }
        }

        private void BtnAddClass_Click(object sender, RoutedEventArgs e) {
            AddClass addClass = new AddClass();
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

        public void WindowAddEditClass_Closed(object sender, EventArgs e) {
            UpdateAgendaView();
        }

        private void Window_Closed(object sender, EventArgs e) {
            BtnSave(sender, new RoutedEventArgs());
        }

        #endregion


    }
}
