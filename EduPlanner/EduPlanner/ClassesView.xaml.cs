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

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for ClassesView.xaml
    /// </summary>
    public partial class ClassesView : Window {
        public ClassesView() {
            InitializeComponent();

            InitializeView();
        }

        private void InitializeView() {
            Schedule schedule = DataManager.schedule;

            foreach (Class _class in schedule.classes) {
                ClassCard classCard = new ClassCard(_class);
                MainView.Children.Add(classCard);
            }
        }
    }
}
