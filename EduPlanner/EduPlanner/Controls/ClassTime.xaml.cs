using System.Windows;
using System.Windows.Controls;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for ClassTime.xaml
    /// </summary>
    public partial class ClassTime : UserControl {

        public delegate void ClassTimeDelegate();

        public event ClassTimeDelegate ClassTimeChanged;

        public ClassTime() {
            InitializeComponent();
        }

        private void RemoveTime_Click(object sender, RoutedEventArgs e) {
            StackPanel panel = Parent as StackPanel;
            panel.Children.Remove(this);
            ClassTimeChanged?.Invoke();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {
            ClassTimeChanged?.Invoke();
        }

        private void TimePicker_Changed(object sender, RoutedEventArgs e) {
            ClassTimeChanged?.Invoke();
        }
    }
}
