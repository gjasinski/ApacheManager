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

namespace Apache_Manager.Controls.Directories
{
    /// <summary>
    /// Interaction logic for editDirectoryPermissions1.xaml
    /// </summary>
    public partial class editDirectoryPermissions : UserControl
    {
        MainWindow mw;
        public editDirectoryPermissions(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.Directories.mainDirectories(mw));
        }
    }
}
