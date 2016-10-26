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

namespace Apache_Manager.Controls.Logs
{
    /// <summary>
    /// Interaction logic for showLog.xaml
    /// </summary>
    public partial class showLog : UserControl
    {
        MainWindow mw;
        Class.logmanager lm = new Class.logmanager();
        Class.registryClass rc = new Class.registryClass();
        int s;
        string fn;
        /// <summary>
        /// Shows logs
        /// </summary>
        /// <param name="m">Handle to previous window</param>
        /// <param name="fileName">File name</param>
        /// <param name="source">Choose directory: 1 - logs from apache, 2 - logs from archive</param>
        public showLog(MainWindow m,  string fileName, int source)
        {
            this.mw = m;
            this.s = source;
            this.fn = fileName;
            InitializeComponent();
            rtbShowLog.SelectAll();
            rtbShowLog.Cut();
            rtbShowLog.AppendText(lm.showLog(fileName, source));
           
            string logDirectory;
            if (source == 1) logDirectory = rc.getValue("logLocation");      //get log directory from registy
            else logDirectory = rc.getValue("backupLocation");
            tbdirectory.Text = logDirectory + "\\" + fileName; 

            if (source == 2)
            {
                clearlog.Visibility = Visibility.Hidden;
                addtoarchove.Visibility = Visibility.Hidden;
            }
            else
            {
                clearlog.Visibility = Visibility.Visible;
                addtoarchove.Visibility = Visibility.Visible;
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.Logs.mainLogs(mw,s));
        }

        private void clearlog_Click(object sender, RoutedEventArgs e)
        {
            if (s == 2) MessageBox.Show("Nie można czyścić logów z archiwum!", "Info");
            else
            {
                if (lm.clearLog(fn))
                    {
                        MessageBox.Show("Log został wyczyszczony", "Info");
                        mw.Content.Children.Clear();
                        mw.Content.Children.Add(new Controls.Logs.showLog(mw, fn, s));
                    }
                    else
                    {
                        MessageBox.Show("BŁĄD: Log nie został wyczyszczony!", "ERROR");
                    };
                }
            
        }

        private void addtoarchove_Click(object sender, RoutedEventArgs e)
        {
            if (s == 2) MessageBox.Show("Nie można archiwizować plików z archiwum!", "Info");
            else
            {
                    if (lm.makeLogBackup(fn))
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

