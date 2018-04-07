using System;

namespace EduPlanner.Classes {

    [Serializable]
    public class Assignment {

        public DateTime DueDate;

        public Class Class;

        public string AssignmentName;

        public string Description;

        public bool Completed;

        public Assignment() { }

        public Assignment(string name, string desc, DateTime time, Class _class) {
            AssignmentName = name;
            Description = desc;
            DueDate = time;
            Class = _class;
            Completed = false;
        }
    }
}
