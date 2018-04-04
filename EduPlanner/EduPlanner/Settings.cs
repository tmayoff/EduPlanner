using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner {

    [Serializable]
    public class Settings {

        public bool checkForUpdatesOnStartup = true;
        public bool minimizeToTray = true;
        public bool receiveBetaUpdates = false;
        public bool useDemoContent = false;

        public Settings() {

        }
    }
}
