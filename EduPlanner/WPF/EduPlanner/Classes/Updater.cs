using System;
using System.Xml;
using System.Windows;
using System.Reflection;

namespace EduPlanner.Classes
{
    public static class Updater
    {

        public static void CheckForUpdate(bool startup = false)
        {

            Version newVersion = null;
            bool betaUpdate = false, requiredUpdate = false;

            string mbNewVersion = string.Empty, mbCurrentVersion = string.Empty, mbText = string.Empty;
            //string xmlUrl = @"D:\Desktop\update.xml";
            string xmlUrl = "https://raw.githubusercontent.com/tyxman/EduPlanner/master/EduPlanner/update.xml";
            string downloadUrl = "https://github.com/tyxman/EduPlanner/releases/";

            string mbHeader = DataManager.APPLICATIONNAME + " Updater";
            string msgError = "An unknown error occured while checking for updates.";

            XmlTextReader reader = new XmlTextReader(xmlUrl);
            try
            {
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
                if (reader.NodeType == XmlNodeType.Element && reader.Name == DataManager.APPLICATIONNAME)
                {
                    while (reader.Read())
                    {
                        // when we find an element node,  
                        // we remember its name  
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            elementName = reader.Name;
                        }
                        else
                        {
                            // for text nodes...  
                            if (reader.NodeType == XmlNodeType.Text && reader.HasValue)
                            {
                                // we check what the name of the node was  
                                switch (elementName)
                                {
                                    case "version":
                                        newVersion = new Version(reader.Value);
                                        break;
                                    case "url":
                                        downloadUrl = reader.Value;
                                        break;
                                    case "beta":
                                        if (reader.Value == "true")
                                        {
                                            betaUpdate = true;
                                        }
                                        break;
                                    case "required":
                                        if (reader.Value == "true")
                                        {
                                            requiredUpdate = true;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception(msgError);
                }


                string[] currentBuild = About.currentVersion.ToString().Split('.');
                if (currentBuild[2] == "0")
                {
                    mbCurrentVersion = About.currentVersion.ToString(2);
                }
                else
                {
                    mbCurrentVersion = string.Format("{0} ({1})", About.currentVersion.ToString(2), currentBuild[2]);
                }

                string[] newBuild = newVersion.ToString().Split('.');
                if (newBuild[2] == "0")
                {
                    mbNewVersion = newVersion.ToString(2);
                }
                else
                {
                    mbNewVersion = string.Format("{0} ({1})", Assembly.GetExecutingAssembly().GetName().Version.ToString(2), newBuild[2]);
                }

                if (About.currentVersion.CompareTo(newVersion) < 0)
                {
                    if (requiredUpdate == true)
                    {
                        mbText = String.Format("New version detected. You must update to continue!\n\n" +
                                              "Current version: {0}\n" +
                                              "New version: {1}", mbCurrentVersion, mbNewVersion);
                        MessageBoxResult result = MessageBox.Show(mbText, mbHeader, MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.OK)
                        {
                            System.Diagnostics.Process.Start(downloadUrl);
                        }
                        Environment.Exit(0);
                    }

                    if (startup == true && DataManager.Settings.CheckForUpdatesOnStartup == true)
                    {
                        if (betaUpdate == true && DataManager.Settings.ReceiveBetaUpdates == true)
                        {
                            BetaUpdateDialog();
                        }
                        if (betaUpdate == false)
                        {
                            UpdateDialog();
                        }
                    }

                    if (startup == false)
                    {
                        if (betaUpdate == true && DataManager.Settings.ReceiveBetaUpdates == true)
                        {
                            BetaUpdateDialog();
                        }
                        if (betaUpdate == false)
                        {
                            UpdateDialog();
                        }

                        if (betaUpdate == true && DataManager.Settings.ReceiveBetaUpdates == false && requiredUpdate == false)
                        {
                            LatestVersionDialog();
                        }
                    }
                }

                else if (About.currentVersion == newVersion && startup == false)
                {
                    LatestVersionDialog();
                }

                else if (About.currentVersion.CompareTo(newVersion) > 0)
                {
                    throw new Exception(msgError);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, mbHeader, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                reader?.Close();
            }

            void BetaUpdateDialog()
            {
                mbText = String.Format("New version detected. Would you like to download it now?\n\n" +
                      "Current version: {0}\n" +
                      "New beta version: {1}", mbCurrentVersion, mbNewVersion);
                MessageBoxResult result = MessageBox.Show(mbText, mbHeader, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(downloadUrl);
                }
            }

            void UpdateDialog()
            {
                mbText = String.Format("New version detected. Would you like to download it now?\n\n" +
                          "Current version: {0}\n" +
                          "New version: {1}", mbCurrentVersion, mbNewVersion);
                MessageBoxResult result = MessageBox.Show(mbText, mbHeader, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(downloadUrl);
                }
            }

            void LatestVersionDialog()
            {
                MessageBox.Show("You are running the latest version of EduPlanner!", mbHeader, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}