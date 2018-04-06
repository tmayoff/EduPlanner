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

namespace EduPlanner.Views {
    /// <summary>
    /// Interaction logic for ClassListView.xaml
    /// </summary>
    public partial class ClassListView : UserControl {

        public ClassListView() {
            InitializeComponent();

            MainWindow.UpdateViews += UpdateView;
        }

        public void UpdateView() {
            panelClassList.Children.Clear();

            foreach (Class _class in DataManager.Schedule.Classes) {
                ClassCard classCard = new ClassCard(_class);
                panelClassList.Children.Add(classCard);
            }
        }
    }
}
