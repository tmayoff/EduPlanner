using System;
using System.Windows;
using System.IO;
using Microsoft.Win32;

namespace EduPlanner {

    [Serializable]
    public class Settings {

        public bool checkForUpdatesOnStartup;
        public bool minimizeToTray;
        public bool receiveBetaUpdates;
        public bool driveIntergration;

        public bool useDemoContent;
    }
}
