using System;
using System.Collections.Generic;

namespace EduPlanner {
    [Serializable]
    public class Class {

        public string className;

        public Dictionary<DayOfWeek, List<DateTime?>> classTimes;

        public List<Homework> homeworks = new List<Homework>();

        public bool hasHomework = false;

        public Class(string className) {
            ResetTimes();
            homeworks = new List<Homework>();

            this.className = className;
        }

        public void SetTime(DayOfWeek day, DateTime start, DateTime end) {
            classTimes[day][0] = start;
            classTimes[day][1] = end;
        }

        public void ResetTimes() {
            classTimes = new Dictionary<DayOfWeek, List<DateTime?>>();

            for (int i = 0; i < DataManager.DAYCOUNT; i++) {
                List<DateTime?> times = new List<DateTime?>() { null, null };
                classTimes.Add((DayOfWeek)i, times);

            }
        }

        public override string ToString() {
            return className;
        }
    }
}
