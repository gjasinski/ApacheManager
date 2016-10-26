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
    /// Interaction logic for filesSelect.xaml
    /// </summary>
    public partial class filesSelect : UserControl
    {
        System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
        public filesSelect()
        {
            InitializeComponent();
        }

        private void fSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.filesName.Text = dialog.InitialDirectory +dialog.FileName;
            }
        }
    }
}
