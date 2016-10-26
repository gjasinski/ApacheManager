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
    /// Interaction logic for mainvirtualhost.xaml
    /// </summary>
    public partial class mainVirtualHost : UserControl
    {
        public MainWindow mw;
        Class.virtualHostManager vhm = new Class.virtualHostManager();
        public mainVirtualHost(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            List<string> vhname= vhm.vhnames();
            foreach (string name in vhname) { lbHost.Items.Add(name); }
            lbHost.SelectedIndex = 0;

        }

        private void addvirtualhost_Click(object sender, RoutedEventArgs e)
        {
            if (lbHost.SelectedItem.ToString() != "Brak Virtual-Hostów")
            {
                mw.Content.Children.Clear();
                mw.Content.Children.Add(new Controls.virtualhost.addvirtualhost(mw));
            }
        }

        private void deletevirtualhost_Click(object sender, RoutedEventArgs e)
        {
            if (lbHost.SelectedItem.ToString() != "Brak Virtual-Hostów")
            {
                vhm.vhdelete(lbHost.SelectedItem.ToString());
                mw.Content.Children.Clear();
                mw.Content.Children.Add(new Controls.virtualhost.mainVirtualHost(mw));
            }
        }

        private void editvirtualhost_Click(object sender, RoutedEventArgs e)
        {
            if (lbHost.SelectedItem.ToString() != "Brak Virtual-Hostów")
            {
                if (lbHost.SelectedIndex != -1)
                {
                    mw.Content.Children.Clear();
                    mw.Content.Children.Add(new Controls.virtualhost.editvirtualhost(mw, lbHost.SelectedItem.ToString()));
                }
            }
        }
    }
}
