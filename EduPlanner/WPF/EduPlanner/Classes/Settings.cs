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

        public static void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bin file (*.bin)|*.bin";
            saveFileDialog.FileName = String.Format("{0} Export ({1})", DataManager.APPLICATIONNAME, DateTime.Today);
            if (saveFileDialog.ShowDialog() == true)
            {
                string exportedFile = saveFileDialog.FileName;
                string currentFile = DataManager.Savefilepath + "\\appdata.bin";
                File.Copy(currentFile, exportedFile);
            }
        }
    }
}
