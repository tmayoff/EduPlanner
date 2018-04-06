using System;
using System.Xml;
using System.Windows;

namespace EduPlanner {
    public static class Updater {
        public static void CheckForUpdate(bool startup = false) {
            string newVersion = string.Empty;
            bool betaUpdate = false;
            string mbText = string.Empty;
            //string xmlUrl = "D:\\Desktop\\update.xml";
            string xmlUrl = "https://raw.githubusercontent.com/tyxman/EduPlanner/master/EduPlanner/update.xml";
            string downloadUrl = "https://github.com/tyxman/EduPlanner/releases/";
            string mbHeader = DataManager.APPLICATIONNAME + " Updater";
            string msgError = "An unknown error occured while checking for updates.";

            XmlTextReader reader = new XmlTextReader(xmlUrl);
            try {
                // simply (and easily) skip the junk at the beginning
                reader.MoveToContent();

                // internal - as the XmlTextReader moves only  
                // forward, we save current xml element name  
                // in elementName variable. When we parse a  
                // text node, we refer to elementName to check  
                // what was the node name  
                string elementName = string.Empty;

                // we check if the xml starts with a proper  
                // "ourfancyapp" element node  
                if (reader.NodeType == XmlNodeType.Element && reader.Name == DataManager.APPLICATIONNAME) {
                    while (reader.Read()) {
                        // when we find an element node,  
                        // we remember its name  
                        if (reader.NodeType == XmlNodeType.Element) {
                            elementName = reader.Name;
                        } else {
                            // for text nodes...  
                            if (reader.NodeType == XmlNodeType.Text && reader.HasValue) {
                                // we check what the name of the node was  
                                switch (elementName) {
                                    case "version":
                                        newVersion = reader.Value;
                                        break;
                                    case "url":
                                        downloadUrl = reader.Value;
                                        break;
                                    case "beta":
                                        if (reader.Value == "true") {
                                            betaUpdate = true;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                } else {
                    throw new Exception(msgError);
                }

                string curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(2);

                if (curVersion.CompareTo(newVersion) < 0) {
                    if (betaUpdate == true && DataManager.Settings.receiveBetaUpdates == true) {
                        mbText = String.Format("New version detected. Would you like to download it now?\n\n" +
                                              "Current version: {0}\n" +
                                              "New beta version: {1}b", curVersion, newVersion);
                        MessageBoxResult result = MessageBox.Show(mbText, mbHeader, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes) {
                            System.Diagnostics.Process.Start(downloadUrl);
                        }
                    }
                    if (betaUpdate == false) {
                        mbText = String.Format("New version detected. Would you like to download it now?\n\n" +
                                                  "Current version: {0}\n" +
                                                  "New version: {1}", curVersion, newVersion);
                        MessageBoxResult result = MessageBox.Show(mbText, mbHeader, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes) {
                            System.Diagnostics.Process.Start(downloadUrl);
                        }
                    }
                    if (betaUpdate == true && DataManager.Settings.receiveBetaUpdates == false && !startup) {
                        MessageBox.Show("You are running the latest version of EduPlanner!", mbHeader, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                } else if (!startup) {
                    if (curVersion == newVersion) {
                        MessageBox.Show("You are running the latest version of EduPlanner!", mbHeader, MessageBoxButton.OK, MessageBoxImage.Information);
                    } else {
                        throw new Exception(msgError);
                    }
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message, mbHeader, MessageBoxButton.OK, MessageBoxImage.Error);
            } finally {
                if (reader != null) {
                    reader.Close();
                }
            }
        }
    }
}