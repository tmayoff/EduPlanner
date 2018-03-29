using System;
using System.Collections.Generic;

namespace EduPlanner {
    [Serializable]
    public class Schedule {

        public List<Day> days;
        public List<Class> classes;

        public Schedule() {
            classes = new List<Class>();

            days = new List<Day>();

            for (int i = 0; i < DataManager.DAYCOUNT; i++) {
                days.Add(new Day((DayOfWeek)i));
            }
        }
    }
}
