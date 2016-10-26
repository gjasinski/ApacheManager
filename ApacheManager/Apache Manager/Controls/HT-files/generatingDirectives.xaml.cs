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

namespace Apache_Manager.Controls.HT_files
{
    /// <summary>
    /// Interaction logic for generatingdirectives.xaml
    /// </summary>
    public partial class generatingDirectives : UserControl
    {
        MainWindow mw;
        public Class.htGenerator htg=new Class.htGenerator();
        public Controls.DirectorySelect.filesSelect fs = new DirectorySelect.filesSelect();
        public generatingDirectives(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            gridSelector.Children.Add(this.fs);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.HT_files.mainHT_files(mw));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            htg.path = this.fs.filesName.Text;
            lbDocs.Items.Clear();
            List<string> lista = htg.showErrorDocuments();
            foreach (string a in lista)
            {
                lbDocs.Items.Add(a.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.htg.addErrorDocument(Convert.ToInt32(this.txtCode.Text), this.txtFile.Text);
                this.lbDocs.Items.Add(this.txtCode.Text + " - " + this.txtFile.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sprawdź poprawność wprowadzonych danych\n"+ex.Message, "Błąd");
            }
            lbDocs.Items.Refresh();
        }
    }
}
