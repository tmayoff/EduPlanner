using System.Collections.Generic;
using System.Windows.Controls;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for ClassHomeworkCard.xaml
    /// </summary>
    public partial class ClassHomeworkCard : UserControl {


        public string ClassName { get; set; }

        public string HomeworkCount {
            get {
                if (_class.homeworks.Count == 0)
                    return "";
                return _class.homeworks.Count.ToString();
            }
        }

        public Class _class;

        public ClassHomeworkCard(Class _class) {
            InitializeComponent();
            DataContext = this;

            this._class = _class;

            ClassName = _class.className;
        }
    }
}
