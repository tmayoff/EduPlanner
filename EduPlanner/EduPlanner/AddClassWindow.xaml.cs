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
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using BespokeFusion;

namespace EduPlanner {

    /// <summary>
    /// Interaction logic for AddClass.xaml
    /// </summary>
    public partial class AddClassWindow : Window {

        List<DayTime> dayTimes;

        public AddClassWindow() {
            InitializeComponent();

            dayTimes = new List<DayTime>();
        }

        private void AddDayTime_Click(object sender, RoutedEventArgs e) {
            DayTime dayTime = new DayTime(this);
            dayTimes.Add(dayTime);
            panelDayTime.Children.Add(dayTime);
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            List<TimePicker> starts = new List<TimePicker>();
            List<TimePicker> ends = new List<TimePicker>();
            List<DayOfWeek> days = new List<DayOfWeek>();

            for (int i = 0; i < dayTimes.Count; i++) {
                //Add the start times
                starts.Add(dayTimes[i].FindName("timeClassStart") as TimePicker);
                ends.Add(dayTimes[i].FindName("timeClassEnd") as TimePicker);

                //Add days
                for (int j = 0; j < dayTimes[i].days.Length; j++) {
                    if (dayTimes[i].days[j])
                        days.Add((DayOfWeek)j);
                }
            }

            DataManager.schedule.classes.Add(new Class(txtClassName.Text, days, starts, ends));

            Close();
        }
    }
}
