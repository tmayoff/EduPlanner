using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner {
    [Serializable]
    public class Day {
        public List<Class> classes;

        public DayOfWeek day;

        public bool hasClass = false;

        public Day(DayOfWeek day) {
            classes = new List<Class>();
            this.day = day;
        }

        public void Order() {
            classes = classes.OrderBy(c => c.classTimes[day][0].Value).ToList();
        }
    }
}
