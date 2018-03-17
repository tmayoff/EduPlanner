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
using MaterialDesignThemes.Wpf;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for DayTime.xaml
    /// </summary>
    public partial class DayTime : UserControl {

        AddClassWindow classWindow;

        public DayTime(AddClassWindow window) {
            InitializeComponent();
            classWindow = window;
        }

        private void Remove_Click(object sender, RoutedEventArgs e) {
            ((Panel)this.Parent).Children.Remove(this);

            Monday.Checked += this.OnContentChanged;
            Tuesday.Checked += this.OnContentChanged;
        }

        private void ContentChanged(object sender, RoutedEventArgs e) {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null) {
                if (checkBox.IsChecked == true) {
                    classWindow.Validate();
                } else
                    classWindow.UnValidate();
            }
        }
    }
}