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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Apache_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool nodeExists;
        private Class.registryClass rc = new Class.registryClass();
        public Class.auth auth = new Class.auth();
        public Window wnd = null;
        public Class.serviceControl sc = new Class.serviceControl();
        public MainWindow()
        {
            InitializeComponent();
            if (rc.checkConfiguration()==false)
            {
                Content.Children.Clear();
                Content.Children.Add(new Controls.AdditionalControls.firstConfiguration(this));
            }
        }

        private void appClose(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void appMinimalize(object sender, RoutedEventArgs e)
        {
            MainWin.WindowState = WindowState.Minimized;
        }
        public void viewHelp(object sender, RoutedEventArgs e)
        {
            if (this.checkWindowNode())
            {
                wnd = new Windows.windowHelp(this);
                wnd.Show();
                this.nodeExists = true;
            }
        }
        public void viewOptions(object sender, RoutedEventArgs e)
        {
            if (this.checkWindowNode())
            {
                wnd = new Windows.login("masterAccess", this);
                wnd.Show();
                this.nodeExists = true;
            }
        }
        public bool checkWindowNode()
        {
            if (this.nodeExists == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btBackup_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.BackupControl.mainBackupControl(this));
        }

        private void viewAbout(object sender, RoutedEventArgs e)
        {
            if (this.checkWindowNode())
            {
                wnd = new Windows.windowAbout(this);
                wnd.Show();
                this.nodeExists = true;
            }
        }

        private void viewStatus(object sender, RoutedEventArgs e)
        {
            //if (this.checkWindowNode())
            //{
            //    wnd = new Windows.windowOptions(this);
            //    wnd.Show();
            //    this.nodeExists = true;
            //}
        }

        private void btModulese_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.module.mainmodules(this));
        }

        private void btVHost_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.virtualhost.mainVirtualHost(this));
        }

        private void btConf_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.Configuration.mainConfiguration(this));
        }

        private void btHTF_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.HT_files.mainHT_files(this));
        }

        private void btAccess_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.AccessControl.mainAccessControl(this));
        }

        private void btNetwork_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.Network.mainNetwork(this));
        }

        private void btHttpd_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.ConfigurationFiles.mainConfigurationFiles(this));
        }

        private void btLogs_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.Logs.mainLogs(this,0));
        }

        private void btHome_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            this.rc.checkConfiguration();
        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(new Controls.AdditionalControls.firstConfiguration(this));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) //restart
        {
            sc.RestartService();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) //stop
        {
            sc.StopService();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) //start
        {
            sc.StartService();
        }
    }
}
