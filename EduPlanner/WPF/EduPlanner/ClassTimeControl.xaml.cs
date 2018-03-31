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
    /// Interaction logic for ClassTime.xaml
    /// </summary>
    public partial class ClassTimeControl : UserControl {

        public ClassTime classTime;

        public delegate void ClassTimeDelegate();

        public event ClassTimeDelegate ClassTimeChanged;

        public ClassTimeControl(ClassTime classTime) {
            InitializeComponent();
            this.classTime = classTime;
        }

        private void RemoveTime_Click(object sender, RoutedEventArgs e) {
            StackPanel panel = Parent as StackPanel;
            panel.Children.Remove(this);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {
            ClassTimeChanged?.Invoke();
        }

        private void TimePicker_Changed(object sender, RoutedEventArgs e) {
            ClassTimeChanged?.Invoke();

            if (tpStartTime.SelectedTime != null)
                classTime.startTime = tpStartTime.SelectedTime.Value;

            if (tpEndTime.SelectedTime != null) {
                classTime.endTime = tpEndTime.SelectedTime.Value;
            }
        }
    }
}
