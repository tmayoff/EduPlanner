using System;
using System.Windows;
using System.Windows.Controls;

namespace EduPlanner.Windows {

    public partial class EditHomeworkWindow : Window {

        private readonly Class _class;
        private readonly Assignment _assignment;

        public EditHomeworkWindow(Class _class, Assignment assignment) {
            InitializeComponent();

            _assignment = assignment;
            this._class = _class;

            txtAssignmentName.Text = assignment.AssignmentName;
            dpDueDate.SelectedDate = assignment.DueDate;
            tpDueTime.SelectedTime = assignment.DueDate;

            Handler();
        }

        #region Event Handlers

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            Handler();
        }

        private void DateTime_Changed(object sender, RoutedEventArgs e) {
            Handler();
        }

        private void BtnSaveHomework_Click(object sender, RoutedEventArgs e) {
            DateTime dateTime = dpDueDate.SelectedDate.Value;
            DateTime timeTime = tpDueTime.SelectedTime.Value;
            DateTime time = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, timeTime.Hour, timeTime.Minute, timeTime.Second);

            _assignment.AssignmentName = txtAssignmentName.Text;
            _assignment.DueDate = time;
            _assignment.Description = txtDescription.Text;
            _assignment.Completed = cbCompleted.IsChecked == true;
            Close();
        }

        private void BtnDeleteHomework_Click(object sender, RoutedEventArgs e) {
            _class.Homeworks.Remove(_assignment);
            Close();
        }

        #endregion

        private void Handler() {
            if (tpDueTime.SelectedTime != null && dpDueDate.SelectedDate != null && txtAssignmentName.Text != "") {
                btnSaveHomework.IsEnabled = true;
            }
        }
    }
}
