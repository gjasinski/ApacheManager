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
using System.Windows.Shapes;

namespace Apache_Manager.Windows
{
    /// <summary>
    /// Interaction logic for windowAbout.xaml
    /// </summary>
    public partial class windowAbout : Window
    {
        public MainWindow parent;
        public windowAbout(MainWindow parent){
            this.parent = parent;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.nodeExists = false;
        }
    }
}
