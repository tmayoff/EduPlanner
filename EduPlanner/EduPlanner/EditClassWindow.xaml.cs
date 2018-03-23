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
    public partial class EditClassWindow : Window {

        List<ClassTime> classTimes = new List<ClassTime>();

        Class _class;

        public EditClassWindow(Class _class) {
            InitializeComponent();

            this._class = _class;
            txtClassName.Text = _class.className;

            for (int i = 0; i < _class.classTimes.Count; i++) {
                if (_class.classTimes[(DayOfWeek)i][0] != null && _class.classTimes[(DayOfWeek)i][1] != null) {

                    ClassTime time = ClassTimeExist(_class.classTimes[(DayOfWeek)i][0]);

                    if (time == null)
                        time = new ClassTime();

                    time.tpStartTime.SelectedTime = _class.classTimes[(DayOfWeek)i][0];
                    time.tpEndTime.SelectedTime = _class.classTimes[(DayOfWeek)i][1];

                    //Loop through class times
                    WrapPanel wrap = time.FindName("wpDays") as WrapPanel;
                    for (int j = 0; j < wrap.Children.Count; j++) {

                        if (wrap.Children[j] is CheckBox) {
                            CheckBox check = wrap.Children[j] as CheckBox;

                            if (check.Content.ToString() == ((DayOfWeek)i).ToString()) {
                                check.IsChecked = true;
                            }
                        }
                    }

                    time.ClassTimeChanged += new ClassTime.ClassTimeDelegate(Handler);
                    classTimes.Add(time);
                    if (!spClassTimesViewer.Children.Contains(time))
                        spClassTimesViewer.Children.Add(time);
                }
            }
        }

        private ClassTime ClassTimeExist(DateTime? start) {
            for (int i = 0; i < classTimes.Count; i++) {
                if (classTimes[i].tpStartTime.SelectedTime == start)
                    return classTimes[i];
            }

            return null;
        }

        private void ClassName_TextChanged(object sender, TextChangedEventArgs e) {
            Handler();
        }

        private void AddTime_Click(object sender, RoutedEventArgs e) {
            ClassTime time = new ClassTime();
            time.ClassTimeChanged += new ClassTime.ClassTimeDelegate(Handler);
            classTimes.Add(time);
            spClassTimesViewer.Children.Add(time);

            Handler();
        }

        private void SaveClass_Click(object sender, RoutedEventArgs e) {
            //Reset
            for (int i = 0; i < DataManager.schedule.days.Count; i++) {
                DataManager.schedule.days[i].classes.Remove(_class);
            }

            _class.ResetTimes();

            //Loop through class times
            for (int i = 0; i < spClassTimesViewer.Children.Count; i++) {
                ClassTime time = spClassTimesViewer.Children[i] as ClassTime;

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

                            _class.className = txtClassName.Text;

                            _class.classTimes[day][0] = startTime;
                            _class.classTimes[day][1] = endTime;

                            DataManager.schedule.days[(int)day].classes.Add(_class);
                            DataManager.schedule.days[(int)day].hasClass = true;
                        }
                    }
                }
            }

            Close();
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
                btnSaveClass.IsEnabled = true;
            else
                btnSaveClass.IsEnabled = false;
        }
    }
}
