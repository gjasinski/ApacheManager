using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace Apache_Manager.Class
{
    class logmanager
    {
        /// <summary>
        /// Returns logs
        /// </summary>
        /// <param name="logname">Name of log to be returned</param>
        /// <param name="source">Choose directory: 1 - logs from apache, 2 - logs from archive</param>
        /// <returns>Returns content of log in string or error</returns>
        public string showLog(string logName, int source)
        {
            try
            {
                registryClass registry = new registryClass();
                string logDirectory;
                if (source==1) logDirectory = registry.getValue("apacheLocation")+"\\logs";      //get log directory from registy
                else logDirectory = registry.getValue("logLocation"); 
                string logFile = logDirectory + "\\" + logName;              //add slash and log name
                if (File.Exists(logFile))
                {
                    FileStream fin = new FileStream(logFile, FileMode.Open);     // 
                    StreamReader fstr_in = new StreamReader(fin);                //opening file
                    string logsData = fstr_in.ReadToEnd();                       //read file
                    fstr_in.Close();                                             //
                    fin.Close();                                                 //close file
                    return logsData;                                             //return data
                } else return "Błąd: Log "+logFile+ " nie istnieje!";
            }
            catch (Exception e)
            {
                MessageBox.Show("Błąd : " + e.Message, "Błąd");
                return "Error: " + e;
                
                 
            }
        }
        /// <summary>
        /// Clearing logs
        /// </summary>
        /// <param name="logname">Name of log to be cleared</param>
        /// <returns>True on success, false on error</returns>
        public bool clearLog(string logName)
        {
            try
            {
                registryClass registry = new registryClass();
                string logDirectory = registry.getValue("apacheLocation")+"\\logs";         //get log directory from registy
                string logFile = logDirectory + "\\" + logName;                 //add slash and log name
                FileStream fout = new FileStream(logFile, FileMode.Create);     //
                StreamWriter fstr_out = new StreamWriter(fout);                 //creating file to save
                fstr_out.Write("");                                             //clar file
                fstr_out.Close();                                               //
                fout.Close();                                                   //close file
                return true;                                                    //return true if ok
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return false;                                                   
            }

        }
        /// <summary>
        /// Creating backups
        /// </summary>
        /// <param name="logName">Name of log which you want to make back up</param>
        /// <returns>True on success, false on error</returns>
        public bool makeLogBackup(string logName)
        {
            try
            {
                registryClass registry = new registryClass();
                string logDirectory = registry.getValue("apacheLocation")+"\\logs";                                     //get log directory from registy
                string logFile = logDirectory + "\\" + logName;                                             //add slash and log name
                string logArchive = registry.getValue("logLocation");                                        //get log archive directory from registy
                DateTime now = DateTime.Now;
                string date = String.Format("{0:dd/MM/yyyy_HH_mm_ss}", now);
                string logArchiveDirectory = logArchive + "\\" + logName.Remove(logName.Length-4,4) +date+".log";//add date in yyyy-mm-dd
                if(File.Exists(logArchiveDirectory))logArchiveDirectory=logArchiveDirectory.Remove(logArchiveDirectory.Length-4,4)+"-H"+DateTime.Now.Hour+"-M"+DateTime.Now.Minute+"-S"+DateTime.Now.Second+".log";                          
                File.Copy(logFile, logArchiveDirectory);
                return true;                                                                                
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: "+e.ToString(), "ERROR");
                return false;                                                                               
            }
        }
        /// <summary>
        /// Deleting logs from archive
        /// </summary>
        /// <param name="logName">Name of log which you want to delete(archive).</param>
        /// <returns>True on success, false on error</returns>
        public bool deleteArchiveLog(string logName)
        {
            try
            {
                registryClass registry = new registryClass();
                string logArchive = registry.getValue("logLocation");
                string logFile = logArchive + "\\" + logName;
                File.Delete(logFile);
                if (File.Exists(logFile)) return false; else return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return false;
            }
        }
    }
}
