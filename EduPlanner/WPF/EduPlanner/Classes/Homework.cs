using System;

namespace EduPlanner {
    [Serializable]
    public class Homework {

        public DateTime dueDate;

        public Class _class;

        public string assignmentName;

        public string description;

        public bool completed;

        public Homework(string name, string desc, DateTime time, Class _class) {
            assignmentName = name;
            description = desc;
            dueDate = time;
            this._class = _class;
            completed = false;
        }
    }
}
