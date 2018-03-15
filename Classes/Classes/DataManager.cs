using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes {
    public static class DataManager {

        public enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };

        public static List<Class> Classes {
            get { return classes; }
        }

        public static List<Class> classes = new List<Class>();
    }
}
