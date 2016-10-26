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

namespace Apache_Manager.Controls.AccessControl
{
    /// <summary>
    /// Interaction logic for mainAccessControl.xaml
    /// </summary>
    public partial class mainAccessControl : UserControl
    {
        MainWindow mw;
        public mainAccessControl(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
        }

        private void ipadressblocking_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.AccessControl.IPAdressBlocking(mw));
        }
    }
}
