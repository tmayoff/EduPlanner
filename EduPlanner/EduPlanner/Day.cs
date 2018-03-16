using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner {

    public class Day {

        public DayOfWeek day;

        public List<Class> classes;

        public Day(DayOfWeek _day) {
            day = _day;
            classes = new List<Class>();
        }

        public override string ToString() {
            return day.ToString();
        }
    }
}
