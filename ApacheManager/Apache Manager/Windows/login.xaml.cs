using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Apache_Manager.Windows
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        private string accessName;
        private MainWindow parent;
        public login(string accessName, MainWindow mw)
        {            
            this.accessName = accessName;   
            InitializeComponent();
            this.loginInfo.Content = "Strefa Bezpieczeństwa : " + this.accessName.ToString();
            passBox.PasswordChar = "|".ToCharArray()[0];
            this.parent = mw;
            if (this.parent.nodeExists)
            {
                this.Close();
            }
            parent.nodeExists = true;
           // this.parent.Content.Children.Clear();
        }
        public void performAuthorisation()
        {
            int id = -1;
            switch (this.accessName)
            {
                case "logAccess":
                    id = 1;
                    break;
                case "backupAccess":
                    id = 2;
                    break;
                case "masterAccess":
                    id = 3;
                    break;
            }
            if (this.parent.auth.checkAccess((byte)id))
            {


                parent.nodeExists = false;
                switch (this.accessName)
                {
                    case "logAccess":
                        this.parent.Content.Children.Clear();
                        this.parent.Content.Children.Add(new Controls.Logs.mainLogs(this.parent,0));
                        break;
                    case "backupAccess":
                        this.parent.Content.Children.Clear();
                        this.parent.Content.Children.Add(new Controls.BackupControl.mainBackupControl(this.parent));
                        break;
                    case "masterAccess":
                        if(this.parent.checkWindowNode()){
                            this.parent.nodeExists=true;
                            this.parent.wnd=new Windows.windowOptions(this.parent);
                            this.parent.wnd.Show();
                        }
                        break;
               
                }
                this.Close();
            }
            if (this.parent.auth.authorise(this.accessName, passBox.Password))
            {
                this.parent.auth.setAccess((byte)id);

                parent.nodeExists = false;
                //this.parent.session
                switch (this.accessName)
                {
                    case "logAccess":
                        this.parent.Content.Children.Clear();
                        this.parent.Content.Children.Add(new Controls.Logs.mainLogs(this.parent,0));
                        break;
                    case "backupAccess":
                        this.parent.Content.Children.Clear();
                        this.parent.Content.Children.Add(new Controls.BackupControl.mainBackupControl(this.parent));
                        break;
                    case "masterAccess":
                        if (this.parent.checkWindowNode())
                        {
                            this.parent.nodeExists = true;
                            this.parent.wnd = new Windows.windowOptions(this.parent);
                            this.parent.wnd.Show();
                        }
                        break;
                }
                this.Close();
            }
            else
            {
                lblInfo.Content = "Dostęp wstrzymany. Podano złe hasło";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.performAuthorisation();
        }

        private void appClose(object sender, RoutedEventArgs e)
        {
            this.parent.nodeExists = false;
            this.Close();
        }
    }
}
