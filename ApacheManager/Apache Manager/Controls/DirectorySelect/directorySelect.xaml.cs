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

namespace Apache_Manager.Controls.DirectorySelect
{
    /// <summary>
    /// Interaction logic for directorySelect.xaml
    /// </summary>
    public partial class directorySelect : UserControl
    {
        System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
        public directorySelect()
        {
            InitializeComponent();
        }

        private void fileSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.fileName.Text = dialog.SelectedPath;
            }
        }
    }
}
