using System;
using System.Collections.Generic;
using System.Linq;

namespace EduPlanner {

    [Serializable]
    public class Day {

        public readonly DayOfWeek WeekDay;

        public Day() { }

        public Day(DayOfWeek weekDay) {
            WeekDay = weekDay;
        }

        public List<Class> GetClasses() {
            List<Class> classes = new List<Class>();

            foreach (Class _class in DataManager.Schedule.Classes) {
                if (_class.ClassTimes[(int)WeekDay][0] != null) {
                    classes.Add(_class);
                }
            }

            classes = classes.OrderBy(c => c.ClassTimes[(int)WeekDay][0].Value).ToList();

            return classes;
        }
    }
}
