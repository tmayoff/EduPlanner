using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner {
    [Serializable]
    public class Settings
    {
        public static bool checkForUpdatesOnStartup = true;
        public static bool minimizeToTray = true;
    }
}
