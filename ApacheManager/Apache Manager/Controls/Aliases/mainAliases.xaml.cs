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

namespace Apache_Manager.Controls.Aliases
{
    /// <summary>
    /// Interaction logic for mainAliases.xaml
    /// </summary>
    public partial class mainAliases : UserControl
    {
        MainWindow mw;
        public mainAliases(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
        }

        private void createaliases_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.Aliases.createAliases(mw));

        }

        private void deletealiases_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.Aliases.deleteAliases(mw));
        }

        private void editaliases_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.Aliases.editAliases(mw));
        }
    }
}
