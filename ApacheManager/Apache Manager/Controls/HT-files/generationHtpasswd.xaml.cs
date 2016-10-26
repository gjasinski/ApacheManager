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
    /// Interaction logic for generationHTPASSWD.xaml
    /// </summary>
    public partial class generationHtpasswd : UserControl
    {
        MainWindow  mw;
        public generationHtpasswd(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.HT_files.mainHT_files(mw));
        }
    }
}
