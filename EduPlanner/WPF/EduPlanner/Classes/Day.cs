using System;
using System.Collections.Generic;
using System.Linq;

namespace EduPlanner {
    [Serializable]
    public class Day {

        //TODO: Generate a list of classes do not store them

        public DayOfWeek day;

        public Day() { }

        public Day(DayOfWeek day) {
            //classes = new List<Class>();
            this.day = day;
        }

        public void Order() {
            //classes = classes.OrderBy(c => c.classTimes[(int)day][0].Value).ToList();
        }

        public List<Class> GetClasses() {
            List<Class> classes = new List<Class>();

            foreach (Class _class in DataManager.Schedule.classes) {
                if (_class.classTimes[(int)day][0] != null) {
                    classes.Add(_class);
                }
            }

            classes = classes.OrderBy(c => c.classTimes[(int)day][0].Value).ToList();

            return classes;
        }
    }
}
