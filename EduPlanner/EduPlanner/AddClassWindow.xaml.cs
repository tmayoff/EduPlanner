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
using MaterialDesignThemes.Wpf;
using BespokeFusion;

namespace EduPlanner {

    /// <summary>
    /// Interaction logic for AddClass.xaml
    /// </summary>
    public partial class AddClassWindow : Window {

        public AddClassWindow() {
            InitializeComponent();
        }

        private void AddDayTime_Click(object sender, RoutedEventArgs e) {
            DayTime dayTime = new DayTime(this);

            panelDayTime.Children.Add(dayTime);
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            bool[] days = new bool[DataManager.DAYCOUNT];

            for (DayOfWeek day = DayOfWeek.Sunday; day <= DayOfWeek.Saturday; day++) {
                CheckBox checkBox = FindName(day.ToString()) as CheckBox;
                days[(int)day] = checkBox.IsChecked == true;
            }

            Close();
        }

        public void Validate() {
            if (txtClassName.Text != "") {
                btnAdd.IsEnabled = true;
            }
        }

        public void UnValidate() {
            btnAdd.IsEnabled = false;
        }
    }
}
