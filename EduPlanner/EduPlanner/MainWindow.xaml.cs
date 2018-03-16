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

            InitializeAgendaView();

            DataManager.schedule = schedule;
        }

        public void InitializeAgendaView() {
            for (int i = 0; i < DataManager.DAYCOUNT; i++) {
                Day day = new Day((DayOfWeek)i);
                schedule.days[i] = day;
                DayCard card = new DayCard(day);
                AgendaView.Children.Add(card);
            }
        }

        public void UpdateView() {

            //Loop through all the days
            for (int i = 0; i < DataManager.DAYCOUNT; i++) {
                StackPanel dayCard = FindName(((DayOfWeek)i).ToString()) as StackPanel;

                //Loop through the classes and check if its on this day
                for (int j = 0; j < schedule.classes.Count; j++) {
                    if (schedule.classes[j].Days[i]) {
                        ClassCard card = new ClassCard(schedule.classes[j]);

                        dayCard.Children.Add(card);
                    }
                }
            }
        }

        private void AddClass_Click(object sender, RoutedEventArgs e) {
            AddClass classWindow = new AddClass();
            classWindow.Closed += new EventHandler(AddClass_Closed);
            classWindow.Show();
        }

        private void AddClass_Closed(object sender, EventArgs e) {
            UpdateView();
        }
    }
}
