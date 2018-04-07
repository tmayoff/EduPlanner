using System;

namespace EduPlanner.Classes {

    [Serializable]
    public class Settings {

        public bool CheckForUpdatesOnStartup;
        public bool MinimizeToTray;
        public bool ReceiveBetaUpdates;
        public bool DriveIntergration;

        public bool UseDemoContent;
    }
}
