using System;
using System.Windows.Controls;

namespace EduPlanner.Views {
    /// <summary>
    /// Interaction logic for TodayView.xaml
    /// </summary>
    public partial class TodayView : UserControl {

        public string ClassName => Environment.UserName;

        public TodayView() {
            InitializeComponent();
            DataContext = this;

            MainWindow.UpdateViews += UpdateView;
        }

        public void UpdateView() {
            panelToday.Children.Clear();

            DayOfWeek today = DateTime.Now.DayOfWeek;

            foreach (Class _class in DataManager.Schedule.Days[(int)today].GetClasses()) {
                ClassCard classCard = new ClassCard(_class, DataManager.Schedule.Days[(int)today]);
                panelToday.Children.Add(classCard);
            }
        }
    }
}
