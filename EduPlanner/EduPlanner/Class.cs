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

        public DateTime? startTime;
        public DateTime? endTime;

        public List<ClassTime> times;

        public Class(string className, DateTime? startTime, DateTime? endTime, List<ClassTime> times) {
            this.times = times;

            this.className = className;
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }
}
