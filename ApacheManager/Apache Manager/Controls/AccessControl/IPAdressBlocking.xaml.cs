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
using System.Net;

namespace Apache_Manager.Controls.AccessControl
{
    /// <summary>
    /// Interaction logic for IPAdressBlocking.xaml
    /// </summary>
    public partial class IPAdressBlocking : UserControl
    {
        MainWindow mw;
        public Class.accessControl ac = new Class.accessControl();
        public Controls.DirectorySelect.filesSelect fs = new DirectorySelect.filesSelect();
        public IPAdressBlocking(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            gridRestrict.Children.Add(this.fs);
            
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.AccessControl.mainAccessControl(mw));
        }

        private void lbAddresses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbAddresses.SelectedItem != null)
            {
                ac.unlockAccess(IPAddress.Parse(lbAddresses.SelectedItem.ToString()));
                lbAddresses.Items.Remove(lbAddresses.SelectedItem);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ac.restrictionFile = this.fs.filesName.Text;
            lbAddresses.Items.Clear();
            List<IPAddress> lista = ac.getBlockedIPList();
            foreach (IPAddress a in lista)
            {
                lbAddresses.Items.Add(a.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                lbAddresses.Items.Add(IPAddress.Parse(txtIP.Text));
                ac.denyAccess_IP(IPAddress.Parse(txtIP.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Podaj prawidłowy adres IP"+ex.ToString(), "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
