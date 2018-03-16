using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private List<Class> classes;

        public MainWindow() {
            InitializeComponent();

            classes = new List<Class>();
        }

        public void UpdateView() {
            for (int i = 0; i < DataManager.DAYCOUNT; i++) {
                classes.Add(DataManager.classes[i]);

                //if (classes.Contains(DataManager.classes[i]))
                //    continue;

                //classes.Add(DataManager.classes[i]);

                //ClassCard card = new ClassCard { Class = classes[i] };

                //AgendaView.Children.Add(card);
            }
        }

        private void AddClass_Click(object sender, RoutedEventArgs e) {
            AddClass classWindow = new AddClass();
            classWindow.Closed += new EventHandler(AddClass_Closed);
            classWindow.Show();
        }

        private void AddClass_Closed(object sender, EventArgs e) {
            UpdateView();
        }
    }
}
