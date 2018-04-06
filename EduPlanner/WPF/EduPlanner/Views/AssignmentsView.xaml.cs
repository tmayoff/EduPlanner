using System;
using System.Windows.Controls;

namespace EduPlanner.Views {
    /// <summary>
    /// Interaction logic for AssignmentsView.xaml
    /// </summary>
    public partial class AssignmentsView : UserControl {

        private readonly DateTime _upcomingTime;

        public AssignmentsView() {
            InitializeComponent();

            _upcomingTime = DateTime.Now + new TimeSpan(7, 0, 0, 0);

            MainWindow.UpdateViews += UpdateView;
        }

        public void UpdateView() {
            panelHomework.Children.Clear();
            panelUpcoming.Children.Clear();

            foreach (Class _class in DataManager.Schedule.Classes) {

                if (_class.Homeworks.Count == 0)
                    continue;

                ClassHomeworkCard currentCard = new ClassHomeworkCard(_class);
                panelHomework.Children.Add(currentCard);

                foreach (Assignment homework in _class.Homeworks) {

                    if (!(currentCard.FindName("classHomework") is StackPanel currentCardPanel))
                        continue;

                    HomeworkCard homeworkCard = new HomeworkCard(currentCard._class, homework, false);
                    currentCardPanel.Children.Add(homeworkCard);

                    if (homework.DueDate > _upcomingTime)
                        continue;

                    homeworkCard = new HomeworkCard(currentCard._class, homework, true);
                    panelUpcoming.Children.Add(homeworkCard);
                }
            }
        }
    }
}
