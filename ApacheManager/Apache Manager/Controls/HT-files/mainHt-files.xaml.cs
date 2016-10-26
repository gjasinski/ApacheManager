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
    /// Interaction logic for mainHT_files.xaml
    /// </summary>
    public partial class mainHT_files : UserControl
    {
        MainWindow mw;
        public mainHT_files(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
        }

        private void generatingdirectives_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.HT_files.generatingDirectives(mw));
        }

        private void generatindhtwasswd_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.HT_files.generationHtpasswd(mw));
        }
    }
}
