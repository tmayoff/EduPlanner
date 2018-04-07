using System.Windows.Controls;
using EduPlanner.Classes;
using EduPlanner.Controls;
using EduPlanner.Windows;

namespace EduPlanner.Views {

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
