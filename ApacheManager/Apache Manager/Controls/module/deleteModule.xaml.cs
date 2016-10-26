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
    /// Interaction logic for deletemodule.xaml
    /// </summary>
    public partial class deleteModule : UserControl
    {
        public MainWindow mw;
        Class.moduleManager mm = new Class.moduleManager();
        SortedDictionary<string,string> modulelist;

        public deleteModule(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            modulelist = mm.returnModuleList(0);
            foreach (KeyValuePair<string,string> line in modulelist)
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

        private void deletemodule_Click(object sender, RoutedEventArgs e)
        {
            if (lbmodules.SelectedIndex == -1)
            {
                MessageBox.Show("Wybierz moduł", "INFO");
            }
            else
            {
                if (mm.deleteModule(lbmodules.SelectedItem.ToString(), modulelist[lbmodules.SelectedItem.ToString()])) MessageBox.Show("Usunieto moduł", "INFO");
                else MessageBox.Show("Błąd", "ERROR");
                mw.Content.Children.Clear();
                mw.Content.Children.Add(new Controls.module.deleteModule(mw));
            }
        }
    }
}
