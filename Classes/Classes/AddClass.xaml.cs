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

namespace Classes {

    /// <summary>
    /// Interaction logic for AddClass.xaml
    /// </summary>
    public partial class AddClass : Window {

        public AddClass() {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e) {

            Class newClass = new Class(txtClassName.Text, timeClassStart, timeClassEnd);
            DataManager.classes.Add(newClass);

            Close();
        }
    }
}
