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

namespace Apache_Manager.Controls.virtualhost
{
    /// <summary>
    /// Interaction logic for editVirtualHost.xaml
    /// </summary>
    public partial class editvirtualhost : UserControl
    {
        public MainWindow mw;
        public string vhname;
        Class.virtualHostManager vhm = new Class.virtualHostManager();
        Controls.DirectorySelect.filesSelect vhselect = new Controls.DirectorySelect.filesSelect();
        string[] data = new string[5];
       

        public editvirtualhost(MainWindow m, string name)
        {
            this.mw = m;
            InitializeComponent();
            this.gridmainfile.Children.Add(vhselect);
            this.vhname = name;
            data = vhm.vhgetdata(vhname);
            tbEmail.Text = data[0];
            vhselect.filesName.Text = data[1];
            tbServiceName.Text = data[2];
            tbIP.Text = data[3];
            tbPort.Text = data[4];
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.virtualhost.mainVirtualHost(mw));
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            int port;
            try
            {
                port = Convert.ToInt32(tbPort.Text);
            }
            catch
            {
                port = 666;
            }
            if (port > 65535)
            {
                port = 666;
                MessageBox.Show("BŁąd: Port musi być mniejszy niz 65536");
            }
            else
            {
                if (vhm.vhdelete(vhname))
                {
                    if (vhm.vhadd(tbEmail.Text, vhselect.filesName.Text, tbServiceName.Text,tbIP.Text, port))
                    {
                        MessageBox.Show("Zmieniono ustawienia VirtualHosta");
                        mw.Content.Children.Clear();
                        mw.Content.Children.Add(new Controls.virtualhost.mainVirtualHost(mw));
                    }
                    else { MessageBox.Show("Błąd"); }
                }
                else
                {
                    MessageBox.Show("BŁĄD:");
                }
            }

        }
    }
}
