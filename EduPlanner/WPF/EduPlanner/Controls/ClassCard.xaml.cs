using System.Windows;
using System.Windows.Controls;
using EduPlanner.Classes;
using EduPlanner.Windows;

namespace EduPlanner.Controls {

    public partial class ClassCard : UserControl {

        public Class _class;
        public Day day;

        public ClassCard(Class _class) {
            InitializeComponent();

            this._class = _class;

            txtClassName.Text = _class.ClassName;
            startTime.Text = "";
            endTime.Text = "";
        }

        public ClassCard(Class _class, Day day) {
            InitializeComponent();

            this._class = _class;
            this.day = day;

            txtClassName.Text = _class.ClassName;

            startTime.Text = "Start Time: " + _class.ClassTimes[(int)day.WeekDay][0].Value.ToString("hh:mm:tt");
            endTime.Text = "End Time: " + _class.ClassTimes[(int)day.WeekDay][1].Value.ToString("hh:mm:tt");
        }

        private void AddHomework_Click(object sender, RoutedEventArgs e) {
            AddAssignmentWindow window = new AddAssignmentWindow(_class, day);
            window.Closed += DataManager.MainWindow.WindowAddEditHomework_Closed;
            window.ShowDialog();
        }

        private void EditClass_Click(object sender, RoutedEventArgs e) {
            EditClassWindow editClass = new EditClassWindow(_class);
            editClass.Closed += DataManager.MainWindow.WindowAddEditClass_Closed;
            editClass.ShowDialog();
        }
    }
}
