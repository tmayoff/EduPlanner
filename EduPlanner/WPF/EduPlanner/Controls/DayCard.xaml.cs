using System.Windows.Controls;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for DayCard.xaml
    /// </summary>
    public partial class DayCard : UserControl {

        public Day day;

        public DayCard(Day day) {
            InitializeComponent();
            
            this.day = day;

            txtDayName.Text = day.day.ToString();
        }
    }
}
