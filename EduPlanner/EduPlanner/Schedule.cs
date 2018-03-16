using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner {
    public class Schedule {

        public List<Class> classes;

        public Schedule() {
            classes = new List<Class>();
        }

        public void AddClass(Class _class) {
            classes.Add(_class);
        }
    }
}
