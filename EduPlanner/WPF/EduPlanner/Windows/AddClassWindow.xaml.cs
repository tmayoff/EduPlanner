using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using EduPlanner.Classes;
using EduPlanner.Controls;

namespace EduPlanner.Windows {

    public partial class AddClassWindow : Window {

        public AddClassWindow() {
            InitializeComponent();
        }

        #region Event Handlers

        private void AddTime_Click(object sender, RoutedEventArgs e) {
            ClassTime time = new ClassTime();
            time.ClassTimeChanged += Handler;
            spClassTimesViewer.Children.Add(time);
            Handler();
        }

        public void AddClass_Click(object sender, RoutedEventArgs e) {

            //Class Times
            foreach (ClassTime time in spClassTimesViewer.Children) {
                if (!(time.FindName("wpDays") is WrapPanel daysPanel))
                    continue;

                if (!(time.FindName("TimeGrid") is Grid timeGrid)) continue;

                //Days
                foreach (CheckBox box in daysPanel.Children) {
                    if (box.IsChecked != true) continue;

                    Enum.TryParse(box.Name, out DayOfWeek day);

                    DateTime? start = (timeGrid.Children[0] as TimePicker).SelectedTime;
                    DateTime? end = (timeGrid.Children[1] as TimePicker).SelectedTime;

                    Class newClass = Schedule.CheckClassExistence(txtClassName.Text);
                    if (newClass == null) {
                        newClass = new Class(txtClassName.Text);
                        DataManager.Schedule.Classes.Add(newClass);
                    }

                    newClass.ClassTimes[(int)day][0] = start;
                    newClass.ClassTimes[(int)day][1] = end;
                }
            }

            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void ClassName_TextChanged(object sender, TextChangedEventArgs e) {
            Handler();
        }

        #endregion

        private void Handler() {
            bool dayChecked = false;
            bool timePicked = false;

            //Loop through class times
            foreach (ClassTime time in spClassTimesViewer.Children) {

                if (!(time.FindName("wpDays") is WrapPanel panel)) continue;
                if (!(time.FindName("TimeGrid") is Grid timeGrid)) continue;

                //Loop through checkboxes
                foreach (CheckBox check in panel.Children) {
                    if (check.IsChecked == true)
                        dayChecked = true;
                }

                //Loop through time pickers
                foreach (TimePicker picker in timeGrid.Children) {
                    timePicked = picker.SelectedTime != null;
                }
            }

            if (dayChecked && timePicked && txtClassName.Text != "")
                btnAddClass.IsEnabled = true;
            else
                btnAddClass.IsEnabled = false;
        }
    }
}