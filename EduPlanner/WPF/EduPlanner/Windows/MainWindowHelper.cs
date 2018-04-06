using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace EduPlanner {
    public partial class MainWindow : Window {

        public string UserName => String.Format("Hello, {0}! Today is {1}, {2} {3}, {4}.", Environment.UserName, DateTime.Now.DayOfWeek, DateTime.Today.ToString("MMMM"), DateTime.Today.Day, DateTime.Now.Year);

        public void ChangeView(Grid newView) {
            for (int i = 0; i < contentViews.Children.Count; i++) {
                if (!(contentViews.Children[i] is Grid grid))
                    continue;

                grid.Visibility = grid.Name == newView.Name ? Visibility.Visible : Visibility.Collapsed;
            }

            UpdateTodayView();
            UpdateAgendaView();
            UpdateClassListView();
            UpdateHomeworkView();
        }

        public void UpdateTodayView() {
            panelToday.Children.Clear();

            DayOfWeek today = DateTime.Now.DayOfWeek;

            foreach (Class _class in DataManager.Schedule.days[(int)today].GetClasses()) {
                ClassCard classCard = new ClassCard(_class, DataManager.Schedule.days[(int)today]);
                panelToday.Children.Add(classCard);
            }
        }

        public void UpdateClassListView() {
            panelClassList.Children.Clear();

            foreach (Class _class in DataManager.Schedule.classes) {
                ClassCard classCard = new ClassCard(_class);
                panelClassList.Children.Add(classCard);
            }
        }

        /// <summary>
        /// Updates the class Schedule view
        /// </summary>
        public void UpdateAgendaView() {
            panelAgenda.Children.Clear();

            foreach (Day day in _schedule.days) {
                List<Class> classes = day.GetClasses();
                if (classes.Count == 0)
                    continue;

                DayCard dayCard = new DayCard(day);
                if (!(dayCard.FindName("ClassesView") is StackPanel dayCardPanel))
                    continue;

                panelAgenda.Children.Add(dayCard);

                foreach (Class _class in classes) {
                    ClassCard card = new ClassCard(_class, day);

                    dayCardPanel.Children.Add(card);
                }
            }

            Refresh();
            UpdateHomeworkView();
        }

        /// <summary>
        /// Updates the panelHomework views
        /// </summary>
        public void UpdateHomeworkView() {
            panelHomework.Children.Clear();
            panelUpcoming.Children.Clear();

            foreach (Class _class in DataManager.Schedule.classes) {

                if (_class.Homeworks.Count == 0)
                    continue;

                ClassHomeworkCard currentCard = new ClassHomeworkCard(_class);
                panelHomework.Children.Add(currentCard);

                foreach (Homework homework in _class.Homeworks) {

                    if (!(currentCard.FindName("classHomework") is StackPanel currentCardPanel))
                        continue;

                    HomeworkCard homeworkCard = new HomeworkCard(currentCard._class, homework, false);
                    currentCardPanel.Children.Add(homeworkCard);

                    if (homework.dueDate > _upcomingTime)
                        continue;

                    homeworkCard = new HomeworkCard(currentCard._class, homework, true);
                    panelUpcoming.Children.Add(homeworkCard);
                }
            }
        }

        /// <summary>
        /// Refresh the view
        /// </summary>
        public void Refresh() {
            DateTime currentDateTime = DateTime.Now;

            _currentClass = null;

            //Go through each day in the panelAgenda view
            for (int i = 0; i < panelAgenda.Children.Count; i++) {

                if (!(panelAgenda.Children[i] is DayCard dayCard))
                    continue;

                Day day = dayCard.day;
                if (!(dayCard.FindName("card") is Grid dayCardCard))
                    continue;

                //If the day we're looking at is today
                if (day.day == DateTime.Today.DayOfWeek) {

                    dayCardCard.Background = Brushes.LightBlue;

                    if (!(dayCard.FindName("ClassesView") is StackPanel classesPanel))
                        continue;

                    //Loop through all the classes in that day
                    for (int j = 0; j < classesPanel.Children.Count; j++) {

                        if (!(classesPanel.Children[j] is ClassCard classCard))
                            continue;

                        Class _class = classCard._class;
                        if (!(classCard.FindName("card") is Card classCardCard))
                            continue;

                        DateTime start = _class.classTimes[(int)day.day][0].Value;
                        DateTime end = _class.classTimes[(int)day.day][1].Value;

                        //If the current class is we're looking at is right now
                        if (currentDateTime.TimeOfDay >= start.TimeOfDay && currentDateTime.TimeOfDay <= end.TimeOfDay) {
                            classCardCard.Background = Brushes.LightGreen;
                            txtCurrentClass.Text = "Current Class: " + _class.className;
                            _currentClass = _class;
                        } else {
                            classCardCard.Background = Brushes.White;

                            //Get next class
                            if (j < classesPanel.Children.Count - 1) {
                                classCard = classesPanel.Children[j + 1] as ClassCard;
                                _class = classCard._class;

                                _currentClass = _class;
                                txtCurrentClass.Text = "panelUpcoming: " + _currentClass.className;
                            } else {
                                _currentClass = null;
                                txtCurrentClass.Text = "";
                            }
                        }
                    }
                } else
                    dayCardCard.Background = Brushes.White;
            }
        }

        #region Handlers

        private void BtnTodayView_Click(object sender, RoutedEventArgs e) {
            ChangeView(todayView);
        }

        private void BtnClassList_Click(object sender, RoutedEventArgs e) {
            ChangeView(classListView);
        }

        /// <summary>
        /// Change to panelAgenda View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewAgenda_Click(object sender, RoutedEventArgs e) {
            ChangeView(agendaView);
        }

        /// <summary>
        /// Change to Assignments View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewAssignments_Click(object sender, RoutedEventArgs e) {
            ChangeView(assignmentsView);
        }

        #endregion
    }
}
