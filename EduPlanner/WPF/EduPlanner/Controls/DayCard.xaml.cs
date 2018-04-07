using System.Windows.Controls;
using EduPlanner.Classes;

namespace EduPlanner.Controls {

    public partial class DayCard : UserControl {

        public Day Day;

        public DayCard(Day day) {
            InitializeComponent();

            Day = day;

            txtDayName.Text = day.WeekDay.ToString();
        }
    }
}
