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
using System.IO.Compression;
using System.IO;
using System.Data;
using System.IO.Packaging;


namespace Apache_Manager.Class
{
    public class Backup
    {
        
        public Class.registryClass registry = new Class.registryClass();
        public string ApacheLocation;
        public string BackupLocation;
        public Backup()
        {
            this.ApacheLocation = registry.getValue("ApacheLocation") + "\\conf";
            this.BackupLocation = registry.getValue("BackupLocation");
        }
 
        public void makeBackup()
        {
            /// <summary>
            /// Making backup from apacheLocaction to backupLocation
            /// name of file is temporary date
            /// </summary>
            
            DateTime now = DateTime.Now;
            string output =String.Format("{0:dd/MM/yyyy_HH_mm_ss}", now);

            try
            {
                
                ZipFile.CreateFromDirectory(@"" + ApacheLocation , @"" + BackupLocation + "\\" + output + ".zip");
            }
                catch (Exception ex)
            {
                MessageBox.Show("Błąd: "+ ex.Message);
            }

            

        }


        public string[] listuj_backupy()
        {
            /// <summary>
            /// Shows all files in backupLocation
            /// </summary>
            

            string[] files = Directory.GetFiles(@"" + BackupLocation + "\\", "*.zip");
           
            return files;
        }
  


    }
}
