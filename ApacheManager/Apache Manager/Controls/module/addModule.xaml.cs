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
    /// Interaction logic for addmodule.xaml
    /// </summary>
    public partial class addModule : UserControl
    {
        public MainWindow mw;
        Class.moduleManager mm = new Class.moduleManager();
        Controls.DirectorySelect.filesSelect fs = new Controls.DirectorySelect.filesSelect();

        public addModule(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            this.moduledirectory.Children.Add(fs);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mw.Content.Children.Clear();
            mw.Content.Children.Add(new Controls.module.mainmodules(mw));
        }

        private void addmodule_Click(object sender, RoutedEventArgs e)
        {
            string fn = fs.filesName.Text;
            if (fn.Contains(".so") && module_name.Text != null)
            {
                string info = mm.addModule(module_name.Text, fn);
                if (info == "true")
                    MessageBox.Show("Dodano moduł", "INFO");
                else
                    MessageBox.Show(info, "ERROR");
            }
            else
            {
                MessageBox.Show("Podaj poprawne dane! Plik musi być z rozszerzeniem *.so");
            }
        }
    }
}
