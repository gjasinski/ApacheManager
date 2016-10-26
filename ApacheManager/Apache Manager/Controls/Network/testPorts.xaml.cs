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
using System.Net.NetworkInformation;
using System.Net;

namespace Apache_Manager.Controls.Network
{     
    /// <summary>
    /// Interaction logic for testPorts.xaml
    /// </summary>
    public partial class testPorts : UserControl
    {
        Class.NetworkInfo ni = new Class.NetworkInfo();
        MainWindow mw;
        public testPorts(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.Network.mainNetwork(mw));
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse(textBox2.Text);

            listBox1.Items.Add(ip + " : " + textBox1.Text + " - " + ni.testPort(ip, Convert.ToInt16(textBox1.Text)));
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
