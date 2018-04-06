using System;
using System.Windows;
using System.Windows.Controls;

namespace EduPlanner {

    public partial class MainWindow : Window {

        public delegate void UpdateView();

        public static event UpdateView UpdateViews;

        public string UserName => "Hello, " + Environment.UserName;

        public void ChangeView(UserControl newView) {
            for (int i = 0; i < contentViews.Children.Count; i++) {
                if (!(contentViews.Children[i] is UserControl control))
                    continue;

                control.Visibility = control.Name == newView.Name ? Visibility.Visible : Visibility.Collapsed;
            }

            UpdateViews?.Invoke();
        }

        #region Handlers

        private void BtnTodayView_Click(object sender, RoutedEventArgs e) {
            ChangeView(todayView);
        }

        private void BtnClassList_Click(object sender, RoutedEventArgs e) {
            ChangeView(classListView);
        }

        private void BtnViewAgenda_Click(object sender, RoutedEventArgs e) {
            ChangeView(agendaView);
        }

        private void BtnViewAssignments_Click(object sender, RoutedEventArgs e) {
            ChangeView(assignmentsView);
        }

        #endregion
    }
}
