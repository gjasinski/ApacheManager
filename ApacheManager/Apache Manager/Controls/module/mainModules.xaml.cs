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

namespace Apache_Manager.Controls.module
{
    /// <summary>
    /// Interaction logic for mainmodules.xaml
    /// </summary>
    public partial class mainmodules : UserControl
    {
        public MainWindow mw;
        public mainmodules(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            Class.moduleManager mm = new Class.moduleManager();
            mm.checkConfigureModules();
        }

        private void addmodule_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.module.addModule(mw));
        }

        private void deletemodule_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.module.deleteModule(mw));
        }

        private void activemodule_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.module.activeModule(mw));
        }

        private void deactiveaddmodule_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.module.deactiveModule(mw));
        }
    }
}
