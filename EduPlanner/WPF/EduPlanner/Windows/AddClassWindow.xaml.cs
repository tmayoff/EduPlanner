using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for AddClass.xaml
    /// </summary>
    public partial class AddClassWindow : Window {

        List<ClassTime> classTimes = new List<ClassTime>();

        public AddClassWindow() {
            InitializeComponent();
        }

        #region Event Handlers

        private void AddTime_Click(object sender, RoutedEventArgs e) {
            ClassTime time = new ClassTime();
            time.ClassTimeChanged += Handler;
            classTimes.Add(time);
            spClassTimesViewer.Children.Add(time);
            Handler();
        }

        public void AddClass_Click(object sender, RoutedEventArgs e) {

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

                        Class newClass = Schedule.CheckClassExistence(txtClassName.Text);
                        if (newClass == null) {
                            newClass = new Class(txtClassName.Text);
                            DataManager.Schedule.classes.Add(newClass);
                        }

                        newClass.classTimes[(int)day][0] = start;
                        newClass.classTimes[(int)day][1] = end;
                    }
                }
            }

            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
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
                btnAddClass.IsEnabled = true;
            else
                btnAddClass.IsEnabled = false;
        }
    }
}