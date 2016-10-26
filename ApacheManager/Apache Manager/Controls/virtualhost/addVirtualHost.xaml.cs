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
    /// Interaction logic for addvirtualhost.xaml
    /// </summary>
    public partial class addvirtualhost : UserControl
    {
        public MainWindow mw;
        Controls.DirectorySelect.filesSelect vhselect = new Controls.DirectorySelect.filesSelect();
        Class.virtualHostManager vhm = new Class.virtualHostManager();

        public addvirtualhost(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            this.gridmainfile.Children.Add(vhselect);
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
            catch{
                port = 666;
            }
            if (port > 65535)
            {
                port = 666;
                MessageBox.Show("BŁąd: Port musi być mniejszy niz 65536");
            }
            else
            {
                if (vhm.vhadd(tbEmail.Text, vhselect.filesName.Text, tbServiceName.Text,tbIP.Text,port))
                {
                    mw.Content.Children.Clear();
                    mw.Content.Children.Add(new Controls.virtualhost.mainVirtualHost(mw));
                    MessageBox.Show("Dodano Virtual-Hosta");
                }
                else MessageBox.Show("BŁąd: Nie dodano VirtualHosta");
            }
        }
    }
}
