using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace Classes {

    enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }

    public class Class {

        public string Name { get; set; }

        public TimePicker StartTime { get; set; }
        public TimePicker EndTime { get; set; }

        public Class(string name, TimePicker startTime, TimePicker endTime) {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override string ToString() {
            return Name;
        }
    }
}
