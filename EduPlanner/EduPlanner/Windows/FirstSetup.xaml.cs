using System;
using System.Collections;
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
using Google.Apis.Drive.v3;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for FirstSetup.xaml
    /// </summary>
    public partial class FirstSetup : Window {

        public string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };

        public FirstSetup() {
            InitializeComponent();
        }

        private void BtnContinue_Click() {

        }
    }
}
