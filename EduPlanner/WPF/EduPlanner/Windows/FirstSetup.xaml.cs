using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for FirstSetup.xaml
    /// </summary>
    public partial class FirstSetup : Window {

        //DropBoxBase DBB;
        //static string authURL = string.Empty;
        //static string accessToken = string.Empty;
        //static string AppKey = "w2y54466acjfjjn";
        //static string name;

        public FirstSetup() {
            InitializeComponent();

        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e) {
            //Authenticate();

            //DropboxClient client = new DropboxClient(accessToken);
            //var task = Task.Run(Run);
            //task.Wait();
            //txtUserName.Text = "Name: " + name;
        }

        //public void Authenticate() {
        //    try {
        //        if (string.IsNullOrEmpty(AppKey)) {
        //            MessageBox.Show("Please enter valid App Key !");
        //            return;
        //        }
        //        if (DBB == null) {
        //            DBB = new DropBoxBase(AppKey, "EduPlanner");

        //            authURL = DBB.GeneratedAuthenticationURL(); // This method must be executed before generating Access Token.  
        //            accessToken = DBB.GenerateAccessToken();
        //        }
        //    } catch (Exception) {
        //        throw;
        //    }
        //}

        //static async Task Run() {
        //    using (var dbx = new DropboxClient(accessToken)) {
        //        var full = await dbx.Users.GetCurrentAccountAsync();
        //        name = full.Name.DisplayName;
        //    }
        //}
    }
}
