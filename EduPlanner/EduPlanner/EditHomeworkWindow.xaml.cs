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
    /// Interaction logic for EditHomeworkWindow.xaml
    /// </summary>
    public partial class EditHomeworkWindow : Window {

        Class _class;
        Homework homework;

        public EditHomeworkWindow(Class _class, Homework homework) {
            InitializeComponent();

            this.homework = homework;
            this._class = _class;

            txtAssignmentName.Text = homework.assignmentName;
            dpDueDate.SelectedDate = homework.dueDate;
            tpDueTime.SelectedTime = homework.dueDate;

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

            homework.assignmentName = txtAssignmentName.Text;
            homework.dueDate = time;
            homework.description = txtDescription.Text;
        }

        private void BtnDeleteHomework_Click(object sender, RoutedEventArgs e) {
            _class.homeworks.Remove(homework);
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
