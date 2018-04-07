using System;
using System.Windows;
using System.Windows.Controls;
using EduPlanner.Classes;

namespace EduPlanner.Windows {

    public partial class AddAssignmentWindow : Window {

        private Class _class;
        private Day Day;

        public AddAssignmentWindow() {
            InitializeComponent();

            cmbClasses.ItemsSource = DataManager.Schedule.Classes;
        }

        public AddAssignmentWindow(Class _class, Day day) {
            InitializeComponent();

            this._class = _class;
            Day = day;

            cmbClasses.ItemsSource = DataManager.Schedule.Classes;
            cmbClasses.SelectedItem = _class;

            dpDueDate.SelectedDate = _class.ClassTimes[(int)day.WeekDay][1];
            tpDueTime.SelectedTime = _class.ClassTimes[(int)day.WeekDay][1];
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            Handler();
        }

        private void DateTime_Changed(object sender, RoutedEventArgs e) {
            Handler();
        }

        private void Handler() {
            if (tpDueTime.SelectedTime != null && dpDueDate.SelectedDate != null && txtAssignmentName.Text != "") {
                if (cmbClasses.SelectedIndex > -1)
                    btnAddHomeWork.IsEnabled = true;
            }
        }

        private void BtnAddHomeWork_Click(object sender, RoutedEventArgs e) {
            DateTime dateTime = dpDueDate.SelectedDate.Value;
            DateTime timeTime = tpDueTime.SelectedTime.Value;
            DateTime time = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, timeTime.Hour, timeTime.Minute, timeTime.Second);

            Assignment assignment = new Assignment(txtAssignmentName.Text, txtDescription.Text, time, _class);
            _class.Homeworks.Add(assignment);

            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void CmbClasses_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _class = cmbClasses.SelectedItem as Class;

            Handler();
        }
    }
}
