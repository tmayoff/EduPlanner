using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for FirstSetup.xaml
    /// </summary>
    public partial class FirstSetup : Window {

        public string[] Scopes = { DriveService.Scope.DriveReadonly };

        public FirstSetup() {
            InitializeComponent();

            var clientID = "c46967aae28af18fcd7e24d263060515351bd109";
            var clientSecret = "xxx";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets { ClientId = clientID, ClientSecret = clientSecret }, Scopes, Environment.UserName, CancellationToken.None, new FileDataStore("EduPlanner.GoogleDrive.Auth.Store")).Result;
        }
    }
}
