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

namespace Apache_Manager.Controls.module
{
    /// <summary>
    /// Interaction logic for disactivemodule.xaml
    /// </summary>
    public partial class disactiveModule : UserControl
    {
        public MainWindow mw;
        Class.moduleManager mm = new Class.moduleManager();
        public disactiveModule(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            Class.registryClass rg = new Class.registryClass();
            string dir = rg.getValue("apacheLocation") + "\\conf\\httpd.conf";
            FileStream fin = new FileStream(dir, FileMode.Open);
            StreamReader fstr_in = new StreamReader(fin);
            string data = fstr_in.ReadToEnd();
            int firstmodule = data.IndexOf("LoadModule ");
            data = data.Remove(0, firstmodule - 2);
            string[] datalines = data.Split('\n');
            string buf;
            foreach (string line in datalines)
            {
                if (line.Contains("LoadModule "))
                {
                    buf = line.Remove(0, line.IndexOf("e ") + 2);
                    buf = buf.Remove(buf.IndexOf(" "));
                    lbmodules.Items.Add(buf);
                }
            }
            fstr_in.Close();
            fin.Close();
            

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.module.mainmodules(mw));
        }

        private void disactivemodule_Click(object sender, RoutedEventArgs e)
        {
            if (mm.disActiveModule(lbmodules.SelectedItem.ToString()))
            {
                mw.Content.Children.Clear();
                mw.Content.Children.Add(new Controls.module.activeModule(mw));
            }
        }
    }
}
