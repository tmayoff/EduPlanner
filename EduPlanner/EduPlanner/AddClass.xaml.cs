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
using System.Windows.Shapes;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for AddClass.xaml
    /// </summary>
    public partial class AddClass : Window {

        List<ClassTime> classTimes = new List<ClassTime>();
        bool[] days = new bool[DataManager.DAYCOUNT];

        public AddClass() {
            InitializeComponent();
        }

        private void AddTime_Click(object sender, RoutedEventArgs e) {
            ClassTime time = new ClassTime();
            time.ClassTimeChanged += new ClassTime.ClassTimeDelegate(Handler);
            classTimes.Add(time);
            spClassTimesViewer.Children.Add(time);
        }

        public void AddClass_Click(object sender, RoutedEventArgs e) {
            for (int i = 0; i < days.Length; i++) {
                if (days[i]) {
                    //Class newClass = new Class();
                    //DataManager.schedule.days[i].classes.Add(newClass);
                }
            }
        }

        private void ClassName_TextChanged(object sender, TextChangedEventArgs e) {
            Handler();
        }

        private void Handler() {
            bool dayChecked = false;
            bool timePicked = false;

            //Loop through class times
            for (int i = 0; i < spClassTimesViewer.Children.Count; i++) {
                ClassTime time = spClassTimesViewer.Children[i] as ClassTime;

                WrapPanel panel = time.FindName("wpDays") as WrapPanel;
                Grid timeGrid = time.FindName("TimeGrid") as Grid;

                //Loop through checkboxes
                for (int j = 0; j < panel.Children.Count; j++) {

                    if (panel.Children[i] is CheckBox) {
                        CheckBox check = panel.Children[j] as CheckBox;
                        if (check.IsChecked == true)
                            dayChecked = true;

                        days[j] = check.IsChecked == true;
                    }
                }

                //Loop through time pickers
                for (int j = 0; j < timeGrid.Children.Count; j++) {
                    if (timeGrid.Children[j] is TimePicker) {
                        TimePicker picker = timeGrid.Children[j] as TimePicker;
                        timePicked = picker.SelectedTime != null;
                    }
                }
            }

            if (dayChecked && timePicked && txtClassName.Text != "")
                btnAddClass.IsEnabled = true;
            else
                btnAddClass.IsEnabled = false;
        }
    }
}
