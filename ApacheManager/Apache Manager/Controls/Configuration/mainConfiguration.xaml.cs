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

namespace Apache_Manager.Controls.Configuration
{
    /// <summary>
    /// Interaction logic for mainconfiguration.xaml
    /// </summary>
    public partial class mainConfiguration : UserControl
    {
        public Class.serverManager sm = new Class.serverManager();
        
        public MainWindow mw;
        public mainConfiguration(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            refresh();
            

            
            

        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /// <summary>
            /// Giving values from text to fucntion showActualValue
            /// </summary>
            
            sm.changeValue(sm.showActualValue("\n" + "ServerRoot "), ServerRootText.Text);
            sm.changeValue(sm.showActualValue("\n" + "Listen "), ListenText.Text);
            sm.changeValue(sm.showActualValue("\n" + "User "), UserText.Text);

            sm.changeValue(sm.showActualValue("\n" + "Group "), GroupText.Text);
            sm.changeValue(sm.showActualValue("\n" + "ServerName "), ServerNameText.Text);
            sm.changeValue(sm.showActualValue("\n" + "ServerAdmin "), ServerAdminText.Text);

            sm.changeValue(sm.showActualValue("\n" + "LogLevel "), LogLevelText.Text);
            sm.changeValue(sm.showActualValue("\n" + "ErrorLog "), ErrorLogText.Text);
            sm.changeValue(sm.showActualValue("\n" + "DefaultType "), DefaultTypeText.Text);
            refresh();


        }

        public void refresh()
        {
            /// <summary>
            /// Fills text boxes
            /// Returns actual value from httpd.conf
            /// </summary>
            
            
            

            try
            {
                ServerRootText.Text = "ServerRoot \"\" "; 
                ListenText.Text = "Listen ";
                UserText.Text = "User ";
                GroupText.Text = "Group ";
                ServerNameText.Text = "ServerName :";
                ServerAdminText.Text = "ServerAdmin ";
                LogLevelText.Text = "LogLevel ";
                ErrorLogText.Text = "ErrorLog ";
                DefaultTypeText.Text = "DefaultType ";

                ServerRootLabel.Content = (sm.showActualValue("\n" + "ServerRoot "));
                ListenLabel.Content = (sm.showActualValue("\n" + "Listen "));
                UserLabel.Content = (sm.showActualValue("\n" + "User "));

                GroupLabel.Content = (sm.showActualValue("\n" + "Group "));
                ServerNameLabel.Content = (sm.showActualValue("\n" + "ServerName "));
                ServerAdminLabel.Content = (sm.showActualValue("\n" + "ServerAdmin "));

                LogLevelLabel.Content = (sm.showActualValue("\n" + "LogLevel "));
                ErrorLogLabel.Content = (sm.showActualValue("\n" + "ErrorLog "));
                DefaultTypeLabel.Content = (sm.showActualValue("\n" + "DefaultType "));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }
       
    }
}
