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

        List<ClassTimeControl> classTimes = new List<ClassTimeControl>();

        public AddClass() {
            InitializeComponent();
        }

        private void AddTime_Click(object sender, RoutedEventArgs e) {
            ClassTimeControl time = new ClassTimeControl();
            ClassTime classTime = new ClassTime(time);
            time.ClassTimeChanged += new ClassTimeControl.ClassTimeDelegate(Handler);
            classTimes.Add(time);
            spClassTimesViewer.Children.Add(time);
        }

        public void AddClass_Click(object sender, RoutedEventArgs e) {
            //Loop through class times
            for (int i = 0; i < spClassTimesViewer.Children.Count; i++) {
                ClassTimeControl time = spClassTimesViewer.Children[i] as ClassTimeControl;

                WrapPanel panel = time.FindName("wpDays") as WrapPanel;
                Grid timeGrid = time.FindName("TimeGrid") as Grid;

                //Loop through checkboxes (days)
                for (int j = 0; j < panel.Children.Count; j++) {

                    //Check the current checkbox
                    if (panel.Children[j] is CheckBox) {
                        CheckBox check = panel.Children[j] as CheckBox;
                        if (check.IsChecked == true) {

                            //Get day as an enum
                            Enum.TryParse(check.Name, out DayOfWeek day);

                            DateTime? startTime = (timeGrid.Children[0] as TimePicker).SelectedTime;
                            DateTime? endTime = (timeGrid.Children[1] as TimePicker).SelectedTime;

                            Class newClass = new Class(txtClassName.Text, startTime, endTime);
                            newClass.times.Add(time.classTime);

                            DataManager.schedule.days[(int)day].classes.Add(newClass);
                            DataManager.schedule.days[(int)day].hasClass = true;

                        }
                    }
                }
            }

            Close();
        }

        private void ClassName_TextChanged(object sender, TextChangedEventArgs e) {
            Handler();
        }

        private void Handler() {
            bool dayChecked = false;
            bool timePicked = false;

            //Loop through class times
            for (int i = 0; i < spClassTimesViewer.Children.Count; i++) {
                ClassTimeControl time = spClassTimesViewer.Children[i] as ClassTimeControl;

                WrapPanel panel = time.FindName("wpDays") as WrapPanel;
                Grid timeGrid = time.FindName("TimeGrid") as Grid;

                //Loop through checkboxes
                for (int j = 0; j < panel.Children.Count; j++) {

                    if (panel.Children[i] is CheckBox) {
                        CheckBox check = panel.Children[j] as CheckBox;
                        if (check.IsChecked == true)
                            dayChecked = true;
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
