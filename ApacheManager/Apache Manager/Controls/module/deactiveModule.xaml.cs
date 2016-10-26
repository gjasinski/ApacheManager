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
using System.IO;

namespace Apache_Manager.Controls.module
{
    /// <summary>
    /// Interaction logic for deactivemodule.xaml
    /// </summary>
    public partial class deactiveModule : UserControl
    {
        public MainWindow mw;
        Class.moduleManager mm = new Class.moduleManager();
        SortedDictionary<string, string> modulelist;

        public deactiveModule(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            modulelist = mm.returnModuleList(2);
            foreach (KeyValuePair<string, string> line in modulelist)
            {
                lbmodules.Items.Add(line.Key);
            }
            lbmodules.SelectedIndex = 0;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            
                mw.Content.Children.Clear();
                mw.Content.Children.Add(new Controls.module.mainmodules(mw));
        }

        private void deactivemodule_Click(object sender, RoutedEventArgs e)
        {
            if (lbmodules.SelectedIndex == -1)
            {
                MessageBox.Show("Wybierz moduł", "INFO");
            }
            else
            {
                if (mm.deactiveModule(lbmodules.SelectedItem.ToString()))
                {
                    mw.Content.Children.Clear();
                    mw.Content.Children.Add(new Controls.module.deactiveModule(mw));
                }
            }
        }
    }
}
