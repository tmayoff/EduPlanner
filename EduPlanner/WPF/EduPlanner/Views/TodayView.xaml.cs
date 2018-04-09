using System;
using System.Windows;
using System.Windows.Controls;
using EduPlanner.Classes;
using EduPlanner.Controls;
using EduPlanner.Windows;

namespace EduPlanner.Views {

    public partial class TodayView : UserControl {

        public string WelcomeText => string.Format("Hello, {0}. Today is {1}, {2} {3}, {4}.", Environment.UserName, DateTime.Now.DayOfWeek, DateTime.Now.ToString("MMMM"), DateTime.Today.Day, DateTime.Now.Year);

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
