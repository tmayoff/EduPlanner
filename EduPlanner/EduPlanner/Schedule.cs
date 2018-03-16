using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner {
    public class Schedule {

        public List<Class> classes;

        public Day[] days;

        public Schedule() {
            classes = new List<Class>();
            days = new Day[DataManager.DAYCOUNT];
        }

        public void AddClass(Class _class) {
            classes.Add(_class);

            for (DayOfWeek day = DayOfWeek.Sunday; day <= DayOfWeek.Saturday; day++) {
                if (_class.Days[(int)day])
                    days[(int)day].classes.Add(_class);
            }
        }
    }
}
