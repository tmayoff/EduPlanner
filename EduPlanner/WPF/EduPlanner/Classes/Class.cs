using System;
using System.Collections.Generic;

namespace EduPlanner {

    [Serializable]
    public class Class {

        public string ClassName { get; set; }

        public DateTime?[][] ClassTimes;

        public List<Assignment> Homeworks = new List<Assignment>();

        public Class() { }

        public Class(string className) {
            InitTimes();
            Homeworks = new List<Assignment>();

            ClassName = className;
        }

        public void SetTime(DayOfWeek day, DateTime start, DateTime end) {
            ClassTimes[(int)day][0] = start;
            ClassTimes[(int)day][1] = end;
        }

        public void InitTimes() {
            ClassTimes = new DateTime?[DataManager.DAYCOUNT][];

            for (int i = 0; i < ClassTimes.Length; i++) {
                ClassTimes[i] = new DateTime?[2];
            }
        }

        public override string ToString() {
            return ClassName;
        }
    }
}
