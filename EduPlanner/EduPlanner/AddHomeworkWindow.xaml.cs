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
    /// Interaction logic for AddHomeworkWindow.xaml
    /// </summary>
    public partial class AddHomeworkWindow : Window {

        Class _class;
        Day day;

        public AddHomeworkWindow(Class _class, Day day) {
            InitializeComponent();

            this._class = _class;
            this.day = day;

            dpDueDate.SelectedDate = _class.classTimes[day.day][1];
            tpDueTime.SelectedTime = _class.classTimes[day.day][1];
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            Handler();
        }

        private void DateTime_Changed(object sender, RoutedEventArgs e) {
            Handler();
        }

        private void Handler() {
            if (tpDueTime.SelectedTime != null && dpDueDate.SelectedDate != null && txtAssignmentName.Text != "") {
                btnAddHomeWork.IsEnabled = true;
            }
        }

        private void BtnAddHomeWork_Click(object sender, RoutedEventArgs e) {
            DateTime dateTime = dpDueDate.SelectedDate.Value;
            DateTime timeTime = tpDueTime.SelectedTime.Value;
            DateTime time = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, timeTime.Hour, timeTime.Minute, timeTime.Second);

            Homework homework = new Homework(txtAssignmentName.Text, txtDescription.Text, time, _class);
            _class.homeworks.Add(homework);
            _class.hasHomework = true;
        }
    }
}
