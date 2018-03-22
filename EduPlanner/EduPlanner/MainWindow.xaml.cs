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

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Schedule schedule;
        Data data;

        public MainWindow() {
            InitializeComponent();

            data = new Data();
            data.Load();

            schedule = DataManager.schedule;
        }

        private void UpdateAgendaView() {
            AgendaView.Children.Clear();

            for (int i = 0; i < schedule.days.Count; i++) {
                if (schedule.days[i].hasClass) {
                    schedule.days[i].Order();

                    DayCard dayCard = new DayCard(schedule.days[i]);
                    StackPanel dayCardPanel = dayCard.FindName("ClassesView") as StackPanel;
                    AgendaView.Children.Add(dayCard);

                    for (int j = 0; j < schedule.days[i].classes.Count; j++) {
                        ClassCard card = new ClassCard(schedule.days[i].classes[j]);
                        dayCardPanel.Children.Add(card);
                    }
                }
            }
        }

        private void BtnAddClass_Click(object sender, RoutedEventArgs e) {
            AddClass addClass = new AddClass();
            addClass.Closed += new EventHandler(WindowAddClass_Closed);
            addClass.ShowDialog();
        }

        private void BtnSave(object sender, RoutedEventArgs e) {
            data.Save();
        }

        private void BtnLoad(object sender, RoutedEventArgs e) {
            data.Load();
            UpdateAgendaView();
        }

        private void WindowAddClass_Closed(object sender, EventArgs e) {
            UpdateAgendaView();
        }
    }
}
