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

namespace Classes {
    /// <summary>
    /// Interaction logic for ClassCard.xaml
    /// </summary>
    public partial class ClassCard : UserControl {

        public Class Class { get; set; }

        public string ClassName { get { return className; } }
        public string StartTime { get { return startTime; } }
        public string EndTime { get { return endTime; } }

        private string className = "Class Name";
        private string startTime = "Start at: ";
        private string endTime = "Ends at: ";

        public ClassCard() {
            InitializeComponent();

            if (Class != null) {
                className = Class.Name;
                startTime = "Starts at: " + Class.StartTime;
                endTime = "Ends at: " + Class.EndTime;
            }

            DataContext = this;
        }
    }
}
