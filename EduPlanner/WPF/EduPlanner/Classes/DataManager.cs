using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Win32;
using File = Google.Apis.Drive.v3.Data.File;

namespace EduPlanner {

    public static class DataManager {

        public static string Savefilepath = @".\EduPlanner\";
        public const string APPLICATIONNAME = "EduPlanner";
        public const int DAYCOUNT = 7;

        public static Schedule Schedule;

        public static Settings Settings;

        public static MainWindow MainWindow;
        public static DriveService Service;

        public static bool Authenticated;

        #region Google Drive

        public static Dictionary<string, string> ConfigValues = XDocument.Load(@"AppSettingConfig.config").Root.Elements().Where(e => e.Name == "add").ToDictionary(
            e => e.Attributes().FirstOrDefault(a => a.Name == "key").Value.ToString(),
            e => e.Attributes().FirstOrDefault(a => a.Name == "value").Value.ToString());

        private static readonly string Id = ConfigValues["clientID"];
        private static readonly string Secret = ConfigValues["clientSecret"];
        private static readonly string mimeType = "application/xml";

        private static readonly string[] Scopes = {
            DriveService.Scope.DriveAppdata
        };

        private static bool _downloadSucceeded;

        public static bool GoogleAuthenticate() {
            try {
                UserCredential credential =
                    GoogleWebAuthorizationBroker.AuthorizeAsync(
                        new ClientSecrets {
                            ClientId = Id,
                            ClientSecret = Secret
                        },
                        Scopes,
                        Environment.UserName,
                        CancellationToken.None,
                        new FileDataStore(@"EduPlanner\GoogleDrive\Auth\Store")).Result;

                Service = new DriveService(new BaseClientService.Initializer {
                    HttpClientInitializer = credential,
                    ApplicationName = "EduPlanner"
                });

            } catch {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Uploads a file to google drive appdata folder
        /// </summary>
        /// <param name="path">The path to the file you want to upload</param>
        public static void UploadFiles(string path) {
            File fileMetadata = new File() {
                Name = Path.GetFileName(path),
                Parents = new List<string>() { "appDataFolder" }
            };

            FilesResource.CreateMediaUpload request;

            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                request = Service.Files.Create(fileMetadata, stream, mimeType);
                request.Fields = "id";
                request.Upload();
            }
        }

        public static void UpdateFiles(string path) {
            string fileName = Path.GetFileName(path);
            string fileId = GetFileId(fileName);

            File file = Service.Files.Get(fileId).Execute();

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            MemoryStream stream = new MemoryStream(bytes);

            FilesResource.UpdateMediaUpload request =
                Service.Files.Update(file, fileId, stream, mimeType);
            request.Upload();
        }

        public static bool DownloadFiles(string fileName) {
            if (!FileExists(fileName))
                return false;

            string fileId = GetFileId(fileName);

            FilesResource.GetRequest request = Service.Files.Get(fileId);
            MemoryStream stream = new MemoryStream();

            request.MediaDownloader.ProgressChanged += DownloadSucceeded;

            request.Download(stream);
            FileStream file = new FileStream(Savefilepath + fileName, FileMode.Create);
            stream.WriteTo(file);
            file.Close();
            stream.Close();
            return _downloadSucceeded;
        }

        public static void DownloadSucceeded(IDownloadProgress progress) {
            _downloadSucceeded = progress.Status == DownloadStatus.Completed;
        }

        public static bool FileExists(string fileName) {
            FilesResource.ListRequest request = DataManager.Service.Files.List();
            request.Spaces = "appDataFolder";
            request.Fields = "nextPageToken, files(id, name)";
            request.PageSize = 10;
            FileList result = request.Execute();
            return result.Files.Any(file => file.Name == fileName);
        }

        private static string GetFileId(string fileName) {
            //Get file id
            FilesResource.ListRequest request = DataManager.Service.Files.List();
            request.Spaces = "appDataFolder";
            request.Fields = "nextPageToken, files(id, name)";
            request.PageSize = 10;
            FileList result = request.Execute();
            foreach (File file in result.Files) {
                if (file.Name == fileName) {
                    return file.Id;
                }
            }

            return null;
        }

        #endregion
    }

    public class Data {

        public int saveTime = 1000;

        private const string APPDATA_NAME = "Appdata.xml";
        private const string SETTINGS_NAME = "Settings.xml";
        private static readonly string AppdataPath = DataManager.Savefilepath + APPDATA_NAME;
        private static readonly string SettingsPath = DataManager.Savefilepath + SETTINGS_NAME;

        public Data() {
            if (DataManager.Schedule == null)
                DataManager.Schedule = new Schedule();
            if (DataManager.Settings == null)
                DataManager.Settings = new Settings();

            LoadAllData();
        }

        public void Save() {
            try {

                if (!Directory.Exists(DataManager.Savefilepath))
                    Directory.CreateDirectory(DataManager.Savefilepath);

                WriteToBinaryFile(AppdataPath, DataManager.Schedule);
                WriteToBinaryFile(SettingsPath, DataManager.Settings);

                if (DataManager.Authenticated) {
                    if (DataManager.FileExists(APPDATA_NAME))
                        DataManager.UpdateFiles(AppdataPath);
                    else
                        DataManager.UploadFiles(AppdataPath);

                    if (DataManager.FileExists(SETTINGS_NAME))
                        DataManager.UpdateFiles(SettingsPath);
                    else
                        DataManager.UploadFiles(SettingsPath);
                }
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        public void LoadAllData() {
            try {
                if (!Directory.Exists(DataManager.Savefilepath))
                    Directory.CreateDirectory(DataManager.Savefilepath);

                if (DataManager.Authenticated) {
                    if (DataManager.FileExists(APPDATA_NAME))
                        DataManager.DownloadFiles(APPDATA_NAME);

                    if (DataManager.FileExists(SETTINGS_NAME))
                        DataManager.DownloadFiles(SETTINGS_NAME);
                }

                if (System.IO.File.Exists(SettingsPath))
                    DataManager.Settings = ReadFromXmlFile<Settings>(SettingsPath);

                if (System.IO.File.Exists(AppdataPath))
                    DataManager.Schedule = ReadFromXmlFile<Schedule>(AppdataPath);
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        public static void Export() {
            SaveFileDialog saveFileDialog = new SaveFileDialog {
                Filter = "Bin file (*.bin)|*.bin",
                FileName = String.Format("{0} Export ({1})", DataManager.APPLICATIONNAME, DateTime.Today)
            };

            if (saveFileDialog.ShowDialog() != true) return;

            string exportedFile = saveFileDialog.FileName;
            string currentFile = DataManager.Savefilepath + APPDATA_NAME;
            System.IO.File.Copy(currentFile, exportedFile);
        }

        public static void Import() {
            try {


                OpenFileDialog openFile = new OpenFileDialog {
                    Filter = "Bin file (*.bin)|*.bin",
                    FileName = APPDATA_NAME
                };

                if (openFile.ShowDialog() == null) return;

                string filePath = openFile.FileName;

                if (DataManager.Authenticated) {
                    if (DataManager.FileExists(APPDATA_NAME))
                        DataManager.UpdateFiles(filePath);
                    else
                        DataManager.UploadFiles(filePath);
                }

                if (System.IO.File.Exists(AppdataPath))
                    DataManager.Schedule = ReadFromXmlFile<Schedule>(filePath);
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        #region Writers / Readers

        /// <summary>
        /// Writes the given object instance to a Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new() {
            TextWriter writer = null;
            try {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            } finally {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static T ReadFromJsonFile<T>(string filePath) where T : new() {
            TextReader reader = null;
            try {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            } finally {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the XML file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false) {
            using (Stream stream = System.IO.File.Open(filePath, append ? FileMode.Append : FileMode.Create)) {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath) {
            using (Stream stream = System.IO.File.Open(filePath, FileMode.Open)) {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new() {
            TextWriter writer = null;
            try {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            } finally {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        public static T ReadFromXmlFile<T>(string filePath) where T : new() {
            TextReader reader = null;
            try {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            } finally {
                if (reader != null)
                    reader.Close();
            }
        }

        #endregion
    }
}
