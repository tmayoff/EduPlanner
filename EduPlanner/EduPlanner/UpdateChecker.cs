using System;
using System.Xml;
using System.Windows;

namespace EduPlanner {
    public class UpdateChecker {
        static string xmlUrl = "https://www.josephdp.com/eduplanner/update.xml";
        static string mbHeader = "Update Checker";
        static string msgError = "An unknown error occured while checking for updates.";

        public static void CheckForUpdate() {
            // in newVersion variable we will store the  
            // version info from xml file  
            string newVersion = string.Empty;
            // and in this variable we will put the url we  
            // would like to open so that the user can  
            // download the new version  
            // it can be a homepage or a direct  
            // link to zip/exe file  
            string downloadUrl = "https://www.josephdp.com/eduplanner/latest.exe";

            // provide the XmlTextReader with the URL of  
            // our xml document  
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
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "EduPlanner") {
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
                                        // thats why we keep the version info  
                                        // in xxx.xxx.xxx.xxx format  
                                        // the Version class does the  
                                        // parsing for us  
                                        newVersion = reader.Value;
                                        break;
                                    case "url":
                                        downloadUrl = reader.Value;
                                        break;
                                    case "release":
                                        if (reader.Value == "no") {
                                            throw new Exception("An update was found, but it has not yet been released.");
                                        }
                                        break;
                                }
                            }
                        }
                    }
                } else {
                    throw new Exception(msgError);
                }

                CompareVersions();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, mbHeader, MessageBoxButton.OK, MessageBoxImage.Error);
            } finally {
                if (reader != null) {
                    reader.Close();
                }
            }

            void CompareVersions() {
                // get the running version  
                string curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
                // compare the versions  
                if (curVersion.CompareTo(newVersion) < 0) {
                    // ask the user if he would like  
                    // to download the new version  

                    string msgText = String.Format("New version detected. Would you like to download it now?\n\nCurrent version: {0}\nNew version: {1}", curVersion, newVersion);
                    MessageBoxResult result = MessageBox.Show(msgText, mbHeader, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes) {
                        // navigate the default web  
                        // browser to our app  
                        // homepage (the url  
                        // comes from the xml content)

                        System.Diagnostics.Process.Start(downloadUrl);
                    }
                } else {
                    //MessageBox.Show("You are running the latest version of EduPlanner!", mbHeader, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
