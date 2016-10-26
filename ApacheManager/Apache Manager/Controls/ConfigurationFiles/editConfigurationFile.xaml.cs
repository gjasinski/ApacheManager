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
    /// Interaction logic for editConfigurationFile.xaml
    /// </summary>
    public partial class editConfigurationFile : UserControl
    {
        MainWindow mw;
        Class.registryClass rg = new Class.registryClass();
        string fn;

        /// <summary>
        /// Add configuration to RichTextBox
        /// </summary>
        /// <param name="m"></param>
        public editConfigurationFile(MainWindow m, string fileName)
        {
            try
            {
                this.mw = m;
                InitializeComponent();
                string dir = rg.getValue("apacheLocation") + "\\conf\\" + fileName;
                this.fn = dir;
                FileStream fin = new FileStream(dir, FileMode.Open);
                StreamReader fstr_in = new StreamReader(fin);
                rtbShowConfig.SelectAll();
                rtbShowConfig.Cut();
                rtbShowConfig.AppendText(fstr_in.ReadToEnd());
                fstr_in.Close();
                fin.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
            }
        }
        /// <summary>
        /// Back to menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btback_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.ConfigurationFiles.mainConfigurationFiles(mw));
        }
        /// <summary>
        /// Save configuration file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btsave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //MessageBox.Show("tekst", "tytuł", System.Windows.Forms.MessageBoxButtons.YesNo);
                string dir = fn;
                FileStream fout = new FileStream(dir, FileMode.Truncate);
                StreamWriter fstr_out = new StreamWriter(fout);
                rtbShowConfig.SelectAll();
                rtbShowConfig.Copy();
                string data = Clipboard.GetText();
                fstr_out.Write(data);
                fstr_out.Close();
                fout.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("BŁĄD: " + ex.ToString(), "ERROR");
            }
        }
    }
}
