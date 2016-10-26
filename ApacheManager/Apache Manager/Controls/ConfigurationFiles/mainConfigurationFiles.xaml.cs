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
using System.IO;

namespace Apache_Manager.Controls.ConfigurationFiles
{
    /// <summary>
    /// Interaction logic for mainConfigurationFiles.xaml
    /// </summary>
    public partial class mainConfigurationFiles : UserControl
    {
        MainWindow mw;
        public mainConfigurationFiles(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            Class.registryClass rg = new Class.registryClass();
            string apacheLogLocation = rg.getValue("apacheLocation") + "\\conf";
            if (!Directory.Exists(apacheLogLocation))
            {
                MessageBox.Show("Brak plików konfiguracyjnych! Przywróć dane z kopi zapasowej", "ERROR");
            }
            else
            {
                string[] files = Directory.GetFiles(apacheLogLocation, "*.conf");
                foreach (string dir in files) lbFiles.Items.Add(dir.Remove(0, apacheLogLocation.Length + 1));
                apacheLogLocation += "\\extra";
                files = Directory.GetFiles(apacheLogLocation, "*.conf");
                if (!Directory.Exists(apacheLogLocation))
                {
                    MessageBox.Show("Brak plików konfiguracyjnych! Przywróć dane z kopi zapasowej", "ERROR");
                }
                else
                {
                    foreach (string dir in files) { lbFiles.Items.Add(dir.Remove(0, apacheLogLocation.Length -5)); }
                }
                
            }
        }

        private void editconfigurationfiles_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.ConfigurationFiles.editConfigurationFile(mw, lbFiles.SelectedItem.ToString()));
        }

        private void lbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
