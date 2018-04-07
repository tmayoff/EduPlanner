using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EduPlanner.Classes {
    [Serializable]
    public class Schedule {

        [XmlIgnore]
        public Day[] Days;

        public List<Class> Classes;

        public Schedule() {
            Classes = new List<Class>();
            Days = new Day[DataManager.DAYCOUNT];

            for (int i = 0; i < Days.Length; i++) {
                Days[i] = new Day((DayOfWeek)i);
            }
        }

        public static Class CheckClassExistence(string name) {
            if (DataManager.Schedule.Classes == null || DataManager.Schedule.Classes.Count == 0)
                return null;

            foreach (Class _class in DataManager.Schedule.Classes) {
                if (_class.ClassName == name)
                    return _class;
            }

            return null;
        }
    }
}
