using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MaterialDesignThemes.Wpf;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private Schedule schedule;

        public MainWindow() {
            InitializeComponent();

            schedule = new Schedule();

            DataManager.schedule = schedule;

            InitializeDays();
        }

        private void InitializeDays() {
            for (int i = 0; i < DataManager.DAYCOUNT; i++) {
                Day day = new Day((DayOfWeek)i);
                schedule.days[i] = day;
            }
        }

        public void UpdateAgendaDays() {
            AgendaView.Children.Clear();

            for (int day = 0; day < DataManager.DAYCOUNT; day++) {
                for (int @class = 0; @class < schedule.classes.Count; @class++) {
                    for (int classDay = 0; classDay < schedule.classes[@class].Days.Count; classDay++) {

                        //The class matches the current day
                        if ((int)schedule.classes[@class].Days[classDay] == day) {
                            DayCard dayCard = null;


                            //The class card hasn't been created
                            if (!schedule.days[day].created) {
                                dayCard = new DayCard(schedule.days[day]);
                                AgendaView.Children.Add(dayCard);
                                schedule.days[day].created = true;
                            } else {
                                object obj = AgendaView.Children[day];
                            }

                            ClassCard classCard = new ClassCard(schedule.classes[@class]);
                            StackPanel dayPanel = dayCard.FindName("DaysPanel") as StackPanel;
                            dayPanel.Children.Add(classCard);
                        }
                    }
                }
            }
        }

        public void UpdateAgendaClasses() {
            UpdateAgendaDays();
        }

        private void ViewAllClasses_Click(object sener, RoutedEventArgs e) {
            ClassesView classesView = new ClassesView();
            classesView.Show();
        }

        private void AddClass_Click(object sender, RoutedEventArgs e) {
            AddClassWindow classWindow = new AddClassWindow();
            classWindow.Closed += new EventHandler(AddClass_Closed);

            classWindow.ShowDialog();
        }

        private void AddClass_Closed(object sender, EventArgs e) {
            UpdateAgendaClasses();
        }
    }
}
