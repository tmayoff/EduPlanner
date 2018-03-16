using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace Classes {

    public class Class {
        public string Name { get; set; }

        public TimePicker StartTime { get; set; }
        public TimePicker EndTime { get; set; }

        public bool[] Days = new bool[DataManager.DAYCOUNT];

        public Class(string name, bool[] days, TimePicker startTime, TimePicker endTime) {
            Name = name;
            Days = days;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override string ToString() {
            return Name;
        }
    }
}
