using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace EduPlanner {

    [Serializable]
    public class Settings {

        public bool checkForUpdatesOnStartup;
        public bool minimizeToTray;
        public bool receiveBetaUpdates;
        public bool driveIntergration;
    }
}
