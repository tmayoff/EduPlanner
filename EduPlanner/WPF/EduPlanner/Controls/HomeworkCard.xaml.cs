using System.Windows;
using System.Windows.Controls;
using EduPlanner.Classes;

namespace EduPlanner.Controls {

    public partial class HomeworkCard : UserControl {

        public string HomeworkTitle => _assignment.AssignmentName;


        private readonly Class _class;
        private readonly Assignment _assignment;

        public HomeworkCard(Class _class, Assignment assignment, bool upcoming) {
            InitializeComponent();
            DataContext = this;

            _assignment = assignment;
            this._class = _class;

            txtDueDate.Text = "Due: " + assignment.DueDate.ToString("dd MMM");
            if (upcoming)
                txtClass.Text = _class.ClassName;
        }

        private void EditHomework_Click(object sender, RoutedEventArgs e) {
            EditHomeworkWindow window = new EditHomeworkWindow(_class, _assignment);
            window.Closed += DataManager.MainWindow.WindowAddEditHomework_Closed;
            window.ShowDialog();
        }
    }
}
