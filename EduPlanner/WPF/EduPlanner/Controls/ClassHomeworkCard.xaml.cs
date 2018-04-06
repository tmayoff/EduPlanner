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
                if (_class.Homeworks.Count == 0)
                    return "";
                return _class.Homeworks.Count.ToString();
            }
        }

        public bool Opened { get; set; }

        public Class _class;

        public ClassHomeworkCard(Class _class) {
            InitializeComponent();
            DataContext = this;

            this._class = _class;

            ClassName = _class.ClassName;
        }

        private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e) {
            Expander ex = sender as Expander;
            Opened = ex.IsExpanded;
        }
    }
}
