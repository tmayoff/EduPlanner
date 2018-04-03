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

        public static void Export()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Bin file (*.bin)|*.bin";
                saveFileDialog.FileName = String.Format("{0} Export ({1})", DataManager.APPLICATIONNAME, DateTime.Today);
                if (saveFileDialog.ShowDialog() == true)
                {
                    string exportedFile = saveFileDialog.FileName;
                    string currentFile = DataManager.Savefilepath + @"\Appdata.bin";
                    File.Copy(currentFile, exportedFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred when exporting data.\n\n" + ex.Message, DataManager.APPLICATIONNAME, MessageBoxButton.OK, MessageBoxImage.Error); 
            }

        }
    }
}
