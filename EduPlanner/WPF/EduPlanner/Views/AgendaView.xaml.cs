using System.Collections.Generic;
using System.Windows.Controls;
using EduPlanner.Classes;
using EduPlanner.Controls;
using EduPlanner.Windows;

namespace EduPlanner.Views {

    public partial class AgendaView : UserControl {

        public AgendaView() {
            InitializeComponent();

            MainWindow.UpdateViews += UpdateView;
        }

        public void UpdateView() {
            panelAgenda.Children.Clear();

            foreach (Day day in DataManager.Schedule.Days) {
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
        }
    }
}
