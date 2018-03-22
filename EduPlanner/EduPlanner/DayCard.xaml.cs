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
