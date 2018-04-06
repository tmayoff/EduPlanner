using System;
using System.Collections.Generic;
using EduPlanner;

namespace EduPlanner {
    [Serializable]
    public class Class {

        public string className;

        public DateTime?[][] classTimes;

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
            classTimes = new DateTime?[DataManager.DAYCOUNT][];

            for (int i = 0; i < classTimes.Length; i++) {
                classTimes[i] = new DateTime?[2];
            }
        }

        public override string ToString() {
            return className;
        }
    }
}
