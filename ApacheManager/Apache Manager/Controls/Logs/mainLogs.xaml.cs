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

namespace Apache_Manager.Controls.Logs
{
    /// <summary>
    /// Interaction logic for mainLogs.xaml
    /// </summary>
    public partial class mainLogs : UserControl
    {
        MainWindow mw;
        Class.logmanager lm = new Class.logmanager();
        /// <summary>
        /// Shows main window of logs
        /// </summary>
        /// <param name="m">Handle to previous window</param>
        /// <param name="source">Sets default index in ComboBox cbsource. By default set it 0.</param>
        public mainLogs(MainWindow m, int source)
        {
            this.mw = m;            
            InitializeComponent();
            cbsource.SelectedIndex = source;
        }
        /// <summary>
        /// Clearing logs in Apache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearlog_Click(object sender, RoutedEventArgs e)
        {
            //To oblige user to choose directory and file
            if (cbsource.SelectedIndex == 0) MessageBox.Show("Wybierz źródło logów!", "Info");
            else
            {
                if (lbLogs.SelectedIndex == -1) MessageBox.Show("Wybierz log!", "Info");
                else
                {
                    if (lm.clearLog(lbLogs.SelectedItem.ToString()))
                    {
                        MessageBox.Show("Log został wyczyszczony", "Info");
                    }
                    else
                    {
                        MessageBox.Show("BŁĄD: Log nie został wyczyszczony!", "ERROR");
                    };
                }
            }
        }
        /// <summary>
        /// Copy file to archive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addtoarchive_Click(object sender, RoutedEventArgs e)
        {
            //To oblige user to choose directory and file
            if (cbsource.SelectedIndex == 0) MessageBox.Show("Wybierz źródło logów!", "Info");
            else
            {
                if (cbsource.SelectedIndex == 2) MessageBox.Show("Nie można archiwizować plików z archiwum!", "Info");
                else
                {
                    if (lbLogs.SelectedIndex == -1) MessageBox.Show("Wybierz log!", "Info");
                    else
                    {
                        if (lm.makeLogBackup(lbLogs.SelectedItem.ToString()))
                        {
                            MessageBox.Show("Log został dodany do archiwum", "Info");
                        }
                        else
                        {
                            MessageBox.Show("BŁĄD: Log nie został dodany do archiwum!", "ERROR");
                        };
                    }
                }
            }
        }
        /// <summary>
        /// Shows logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showlog_Click(object sender, RoutedEventArgs e)
        {
            //To oblige user to choose directory and file
            if (cbsource.SelectedIndex == 0) MessageBox.Show("Wybierz źródło logów!", "Info");
            else
            {
                if (lbLogs.SelectedIndex == -1) MessageBox.Show("Wybierz log!", "Info");
                else
                {
                    mw.Content.Children.Clear();
                    mw.Content.Children.Add(new Controls.Logs.showLog(mw, lbLogs.SelectedItem.ToString(), cbsource.SelectedIndex));
                }
            }
        }
        /// <summary>
        /// Showing, hiding buttons and loading data to listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbsource_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Class.registryClass registry = new Class.registryClass();
            string apacheLogLocation = registry.getValue("apacheLocation")+"\\logs\\";
            string archiveLocation = registry.getValue("logLocation");
            if (!Directory.Exists(apacheLogLocation)) Directory.CreateDirectory(apacheLogLocation);
            if (!Directory.Exists(archiveLocation)) Directory.CreateDirectory(archiveLocation);
            try
            {
                string[] apacheLogFiles = Directory.GetFiles(apacheLogLocation,"*.log");
                string[] archiveLogFiles = Directory.GetFiles(archiveLocation,"*.log");
                if (cbsource.SelectedIndex == 1)
                {
                    lbLogs.Items.Clear();
                    foreach (string dir in apacheLogFiles) { lbLogs.Items.Add(dir.Remove(0, apacheLogLocation.Length)); }
                    deletearchive.Visibility = Visibility.Hidden;
                    addtoarchive.Visibility = Visibility.Visible;
                    clearlog.Visibility = Visibility.Visible;

                }
                if (cbsource.SelectedIndex == 2)
                {
                    lbLogs.Items.Clear();
                    foreach (string dir in archiveLogFiles) { lbLogs.Items.Add(dir.Remove(0, archiveLocation.Length+1)); }
                    addtoarchive.Visibility = Visibility.Hidden;
                    clearlog.Visibility = Visibility.Hidden;
                    deletearchive.Visibility = Visibility.Visible;
                }

            }
            catch (Exception ex) 
            {
                
            }
        }
        /// <summary>
        /// Deleting log file from archive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletearchive_Click(object sender, RoutedEventArgs e)
        {
            //To oblige user to choose directory and file
            if (cbsource.SelectedIndex == 0) MessageBox.Show("Wybierz źródło logów!", "Info");
            else
            {
                if (cbsource.SelectedIndex == 1) MessageBox.Show("Nie można usuwać logów z Apacha!", "ERROR");
                else
                {
                    if (lbLogs.SelectedIndex == -1) MessageBox.Show("Wybierz log!", "Info");
                    else
                    {
                        if (lm.deleteArchiveLog(lbLogs.SelectedItem.ToString()))
                        {
                            MessageBox.Show("Log został usunięty z archiwum", "Info");
                            mw.Content.Children.Clear();
                            mw.Content.Children.Add(new Controls.Logs.mainLogs(mw, cbsource.SelectedIndex));
                        }
                        else
                        {
                            MessageBox.Show("BŁĄD: Log nie został usunięty z archiwum!", "ERROR");
                        };
                    }
                }
            }
        }
    }
}
