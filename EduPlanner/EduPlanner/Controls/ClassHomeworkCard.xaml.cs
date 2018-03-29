using System.Collections.Generic;
using System.Windows.Controls;

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

            homeworks = new List<Homework>();

            this._class = _class;


            ClassName = _class.className;
        }
    }
}
