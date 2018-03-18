using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace EduPlanner {

    public class Class {
        public string Name { get; set; }

        public List<TimePicker> StartTimes { get; set; }
        public List<TimePicker> EndTimes { get; set; }

        public List<DayOfWeek> Days { get; set; }

        public Class(string name, List<DayOfWeek> days, List<TimePicker> startTimes, List<TimePicker> endTimes) {
            Name = name;
            Days = days;

            StartTimes = new List<TimePicker>();
            EndTimes = new List<TimePicker>();

            StartTimes.AddRange(startTimes.ToArray());
            EndTimes.AddRange(endTimes.ToArray());
        }
    }
}
