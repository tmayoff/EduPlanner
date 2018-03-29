using System.Windows;
using System.Windows.Controls;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for Class.xaml
    /// </summary>
    public partial class ClassCard : UserControl {

        public Class _class;
        public Day day;

        public ClassCard(Class _class, Day day) {
            InitializeComponent();

            this._class = _class;
            this.day = day;

            txtClassName.Text = _class.className;

            startTime.Text = "Start Time: " + _class.classTimes[day.day][0].Value.ToString("hh:mm:tt");
            endTime.Text = "End Time: " + _class.classTimes[day.day][1].Value.ToString("hh:mm:tt");
        }

        private void AddHomework_Click(object sender, RoutedEventArgs e) {
            AddHomeworkWindow window = new AddHomeworkWindow(_class, day);
            window.Closed += DataManager.mainWindow.WindowAddEditHomework_Closed;
            window.ShowDialog();
        }

        private void EditClass_Click(object sender, RoutedEventArgs e) {
            EditClassWindow editClass = new EditClassWindow(_class);
            editClass.Closed += DataManager.mainWindow.WindowAddEditClass_Closed;
            editClass.ShowDialog();
        }
    }
}
