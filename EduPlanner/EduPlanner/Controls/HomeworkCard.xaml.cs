using System.Windows;
using System.Windows.Controls;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for HomeworkCard.xaml
    /// </summary>
    public partial class HomeworkCard : UserControl {

        public string HomeworkTitle => _homework.assignmentName;


        readonly Class _class;
        readonly Homework _homework;

        public HomeworkCard(Class _class, Homework homework, bool upcoming) {
            InitializeComponent();
            DataContext = this;

            _homework = homework;
            this._class = _class;

            txtDueDate.Text = "Due: " + homework.dueDate.ToString("dd MMM");
            if (upcoming)
                txtClass.Text = _class.className;
        }

        private void EditHomework_Click(object sender, RoutedEventArgs e) {
            EditHomeworkWindow window = new EditHomeworkWindow(_class, _homework);
            window.Closed += DataManager.mainWindow.WindowAddEditHomework_Closed;
            window.ShowDialog();
        }
    }
}
