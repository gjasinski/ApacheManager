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

namespace Apache_Manager.Controls.Network
{
    /// <summary>
    /// Interaction logic for mainNetwork.xaml
    /// </summary>
    public partial class mainNetwork : UserControl
    {
        MainWindow mw;
        public mainNetwork(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
        }

        private void testports_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.Network.testPorts(mw));
        }

        private void networktools_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.Network.networkTools(mw));
        }
    }
}
