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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for HomeworkCard.xaml
    /// </summary>
    public partial class HomeworkCard : UserControl {

        public string HomeworkTitle {
            get {
                return homework.assignmentName;
            }
        }

        Class _class;
        Homework homework;

        public HomeworkCard(Class _class, Homework homework) {
            InitializeComponent();
            DataContext = this;

            this.homework = homework;
            this._class = _class;
        }

        private void EditHomework_Click(object sender, RoutedEventArgs e) {
            EditHomeworkWindow window = new EditHomeworkWindow(_class, homework);
            window.Closed += DataManager.mainWindow.WindowAddEditHomework_Closed;
            window.ShowDialog();
        }
    }
}
