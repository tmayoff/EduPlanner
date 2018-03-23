using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner {
    [Serializable]
    public class Homework {

        public string assignmentName;

        public string description;

        public DateTime dueDate;

        public Class _class;

        public Homework(string name, string desc, DateTime time, Class _class) {
            assignmentName = name;
            description = desc;
            dueDate = time;
            this._class = _class;
        }
    }
}
