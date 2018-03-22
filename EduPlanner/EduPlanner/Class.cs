using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace EduPlanner {
    [Serializable]
    public class Class {

        public string className;

        public Dictionary<DayOfWeek, List<DateTime?>> classTimes;

        public Class(string className) {
            classTimes = new Dictionary<DayOfWeek, List<DateTime?>>();

            for (int i = 0; i < DataManager.DAYCOUNT; i++) {
                List<DateTime?> times = new List<DateTime?>() { null, null };
                classTimes.Add((DayOfWeek)i, times);

            }

            this.className = className;
        }

        public void SetTime(DayOfWeek day, DateTime start, DateTime end) {
            classTimes[day][0] = start;
            classTimes[day][1] = end;
        }
    }
}
