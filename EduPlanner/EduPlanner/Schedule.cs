using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner {
    [Serializable]
    public class Schedule {

        public List<Day> days;

        public Schedule() {
            days = new List<Day>();

            for (int i = 0; i < DataManager.DAYCOUNT; i++) {
                days.Add(new Day((DayOfWeek)i));
            }
        }
    }
}
