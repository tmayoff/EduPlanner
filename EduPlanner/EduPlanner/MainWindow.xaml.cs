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

            for (int i = 0; i < AgendaView.Children.Count; i++) {

                DayCard child = AgendaView.Children[i] as DayCard;
                StackPanel dayPanel = child.FindName("DaysPanel") as StackPanel;
                for (int j = 0; j < dayPanel.Children.Count; j++) {
                    dayPanel.Children.RemoveAt(j);
                }

                foreach (Class _class in child.Day.classes) {
                    if (_class.Days[i]) {
                        ClassCard classCard = new ClassCard(_class);
                        dayPanel.Children.Add(classCard);
                    }
                }
            }
        }

        private void ViewAllClasses_Click(object sener, RoutedEventArgs e) {

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
