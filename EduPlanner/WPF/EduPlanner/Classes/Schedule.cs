using System;
using System.Collections.Generic;

namespace EduPlanner {
    [Serializable]
    public class Schedule {

        public Day[] days;
        //public List<Day> days;
        public List<Class> classes;

        public Schedule() {
            classes = new List<Class>();
            days = new Day[DataManager.DAYCOUNT];

            for (int i = 0; i < days.Length; i++) {
                days[i] = new Day((DayOfWeek)i);
            }
        }

        public static Class CheckClassExistence(string name) {
            if (DataManager.Schedule.classes != null) {
                for (int i = 0; i < DataManager.Schedule.classes.Count; i++) {
                    if (DataManager.Schedule.classes[i].className == name)
                        return DataManager.Schedule.classes[i];
                }
            }

            return null;
        }
    }
}
