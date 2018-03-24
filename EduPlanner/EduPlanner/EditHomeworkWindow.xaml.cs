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
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            _class.homeworks.Remove(homework);
            Close();
        }
    }
}
