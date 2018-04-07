using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EduPlanner.Classes;
using EduPlanner.Controls;

namespace EduPlanner.Windows {

    public partial class EditClassWindow : Window {

        private readonly List<ClassTime> _classTimes = new List<ClassTime>();

        private readonly Class _class;

        public EditClassWindow(Class _class) {
            InitializeComponent();

            this._class = _class;
            txtClassName.Text = _class.ClassName;

            for (int i = 0; i < _class.ClassTimes.Length; i++) {
                if (_class.ClassTimes[i][0] == null || _class.ClassTimes[i][1] == null) continue;
                ClassTime time = ClassTimeExist(_class.ClassTimes[i][0]) ?? new ClassTime();

                time.tpStartTime.SelectedTime = _class.ClassTimes[i][0];
                time.tpEndTime.SelectedTime = _class.ClassTimes[i][1];

                //Loop through class times
                WrapPanel wrap = time.FindName("wpDays") as WrapPanel;
                foreach (CheckBox check in wrap.Children) {
                    if (check.Content.ToString() == ((DayOfWeek)i).ToString()) {
                        check.IsChecked = true;
                    }
                }

                time.ClassTimeChanged += Handler;
                _classTimes.Add(time);
                if (!spClassTimesViewer.Children.Contains(time))
                    spClassTimesViewer.Children.Add(time);
            }
        }

        private ClassTime ClassTimeExist(DateTime? start) {
            foreach (ClassTime time in _classTimes) {
                if (time.tpStartTime.SelectedTime == start)
                    return time;
            }

            return null;
        }

        private void ClassName_TextChanged(object sender, TextChangedEventArgs e) {
            Handler();
        }

        private void AddTime_Click(object sender, RoutedEventArgs e) {
            ClassTime time = new ClassTime();
            time.ClassTimeChanged += Handler;
            _classTimes.Add(time);
            spClassTimesViewer.Children.Add(time);

            Handler();
        }

        private void BtnDeleteClass_Click(object sender, RoutedEventArgs e) {

            DataManager.Schedule.Classes.Remove(_class);

            Close();
        }

        private void SaveClass_Click(object sender, RoutedEventArgs e) {

            //Class Times
            foreach (ClassTime time in spClassTimesViewer.Children) {
                WrapPanel daysPanel = time.FindName("wpDays") as WrapPanel;
                Grid timeGrid = time.FindName("TimeGrid") as Grid;

                //Days
                foreach (CheckBox box in daysPanel.Children) {
                    if (box.IsChecked == true) {

                        Enum.TryParse(box.Name, out DayOfWeek day);

                        DateTime? start = (timeGrid.Children[0] as TimePicker).SelectedTime;
                        DateTime? end = (timeGrid.Children[1] as TimePicker).SelectedTime;

                        _class.ClassTimes[(int)day][0] = start;
                        _class.ClassTimes[(int)day][1] = end;
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
