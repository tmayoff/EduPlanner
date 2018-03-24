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
    /// Interaction logic for ClassHomeworkCard.xaml
    /// </summary>
    public partial class ClassHomeworkCard : UserControl {

        public List<Homework> homeworks;

        public string ClassName { get; set; }
        public string HomeworkCount {
            get {
                if (homeworks.Count == 0)
                    return "";
                return homeworks.Count.ToString();
            }
        }

        public Class _class;

        public ClassHomeworkCard(Class _class) {
            InitializeComponent();
            DataContext = this;

            this._class = _class;

            homeworks = new List<Homework>();

            ClassName = _class.className;
        }
    }
}
