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
    /// Interaction logic for Class.xaml
    /// </summary>
    public partial class ClassCard : UserControl {

        public Class _class;

        public ClassCard(Class _class) {
            InitializeComponent();

            this._class = _class;

            txtClassName.Text = "Class: " + _class.className;

            startTime.Text = "Start Time: " + _class.startTime.Value.ToString("hh:mm:tt");
            endTime.Text = "End Time: " + _class.endTime.Value.ToString("hh:mm:tt");
        }
    }
}
