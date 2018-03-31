using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;

namespace EduPlanner {

    public static class DataManager {

        public static string Savefilepath = @".\EduPlanner";
        public const string APPLICATIONNAME = "EduPlanner";
        public const int DAYCOUNT = 7;

        public static Schedule schedule;

        public static Settings settings;

        public static MainWindow mainWindow;
        public static DriveService service;

        #region Google Drive

        public static Dictionary<string, string> ConfigValues = XDocument.Load(@"..\..\AppSettingConfig.config").Root.Elements().Where(e => e.Name == "add").ToDictionary(
            e => e.Attributes().FirstOrDefault(a => a.Name == "key").Value.ToString(),
            e => e.Attributes().FirstOrDefault(a => a.Name == "value").Value.ToString());

        private static readonly string Id = ConfigValues["clientID"];
        private static readonly string Secret = ConfigValues["clientSecret"];
        private static readonly string mimeType = "application/bin";

        private static readonly string[] Scopes = {
            DriveService.Scope.DriveAppdata
        };

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

                DataManager.service = new DriveService(new BaseClientService.Initializer {
                    HttpClientInitializer = credential,
                    ApplicationName = "EduPlanner"
                });

            } catch (Exception ex) {
                return false;
            }

            return true;
        }

        public static void UploadFiles(string path) {
            File fileMetadata = new File() {
                Name = "Appdata.bin",
                Parents = new List<string>() { "appDataFolder" }
            };

            FilesResource.CreateMediaUpload request;

            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                request = service.Files.Create(fileMetadata, stream, mimeType);
                request.Fields = "id";
                request.Upload();
            }
        }

        public static void UpdateFiles(string path) {
            string fileName = Path.GetFileName(path);
            string fileId = GetFileId(fileName);

            File file = service.Files.Get(fileId).Execute();

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            MemoryStream stream = new MemoryStream(bytes);

            FilesResource.UpdateMediaUpload request =
                service.Files.Update(file, fileId, stream, mimeType);
            request.Upload();
        }

        public static bool DownloadFiles(string fileName) {
            if (!FileExists(fileName))
                return false;

            string fileId = GetFileId(fileName);

            FilesResource.GetRequest request = DataManager.service.Files.Get(fileId);
            MemoryStream stream = new MemoryStream();

            bool succeded = false;

            request.MediaDownloader.ProgressChanged += progress => {
                switch (progress.Status) {
                    case DownloadStatus.Downloading:
                        break;
                    case DownloadStatus.Completed:
                        succeded = true;
                        break;
                    case DownloadStatus.Failed:
                        succeded = false;
                        break;
                }
            };

            if (!succeded)
                return false;

            request.Download(stream);
            FileStream file = new FileStream("./Downloaded", FileMode.Create);
            stream.WriteTo(file);
            file.Close();
            stream.Close();
            return true;
        }

        public static bool FileExists(string fileName) {
            FilesResource.ListRequest request = DataManager.service.Files.List();
            request.Spaces = "appDataFolder";
            request.Fields = "nextPageToken, files(id, name)";
            request.PageSize = 10;
            FileList result = request.Execute();
            return result.Files.Any(file => file.Name == fileName);
        }

        private static string GetFileId(string fileName) {
            //Get file id
            FilesResource.ListRequest request = DataManager.service.Files.List();
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

        private readonly string _appdataPath = DataManager.Savefilepath + @"\Appdata.bin";
        private readonly string _settingsPath = DataManager.Savefilepath + @"\Settings.bin";

        public Data() {
            if (DataManager.schedule == null)
                DataManager.schedule = new Schedule();
            if (DataManager.settings == null)
                DataManager.settings = new Settings();
        }

        public void Save() {
            string appdataName = Path.GetFileName(_appdataPath);
            string settingsName = Path.GetFileName(_settingsPath);

            if (!Directory.Exists(DataManager.Savefilepath))
                Directory.CreateDirectory(DataManager.Savefilepath);

            WriteToBinaryFile(_appdataPath, DataManager.schedule);
            WriteToBinaryFile(_settingsPath, DataManager.settings);

            if (!DataManager.settings.driveIntergration) return;

            if (DataManager.FileExists(appdataName))
                DataManager.UpdateFiles(_appdataPath);
            else
                DataManager.UploadFiles(_appdataPath);

            if (DataManager.FileExists(settingsName))
                DataManager.UpdateFiles(_settingsPath);
            else
                DataManager.UploadFiles(_settingsPath);
        }

        public void Load() {
            if (DataManager.FileExists(Path.GetFileName(_appdataPath)))
                DataManager.DownloadFiles(Path.GetFileName(_appdataPath));

            if (DataManager.FileExists(Path.GetFileName(_settingsPath)))
                DataManager.DownloadFiles(Path.GetFileName(_settingsPath));

            if (System.IO.File.Exists(_settingsPath))
                DataManager.settings = ReadFromBinaryFile<Settings>(_settingsPath);

            if (System.IO.File.Exists(_appdataPath))
                DataManager.schedule = ReadFromBinaryFile<Schedule>(_appdataPath);
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
