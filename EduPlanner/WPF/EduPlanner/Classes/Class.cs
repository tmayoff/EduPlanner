using System;
using System.Collections.Generic;
using EduPlanner;

namespace EduPlanner {
    [Serializable]
    public class Class {

        public string className;

        public List<DateTime?>[] classTimes;
        //public Dictionary<DayOfWeek, List<DateTime?>> classTimes;

        public List<Homework> Homeworks = new List<Homework>();

        public bool hasHomework = false;

        public Class() { }

        public Class(string className) {
            ResetTimes();
            Homeworks = new List<Homework>();

            this.className = className;
        }

        public void SetTime(DayOfWeek day, DateTime start, DateTime end) {
            classTimes[(int)day][0] = start;
            classTimes[(int)day][1] = end;
        }

        public void ResetTimes() {
            classTimes = new List<DateTime?>[DataManager.DAYCOUNT];

            for (int i = 0; i < classTimes.Length; i++) {
                List<DateTime?> times = new List<DateTime?>() { null, null };
                classTimes[i].AddRange(times);
            }
        }

        public override string ToString() {
            return className;
        }
    }
}
