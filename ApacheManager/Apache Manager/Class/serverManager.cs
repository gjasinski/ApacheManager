
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Apache_Manager.Class
{
     public class serverManager
    {
        public string showActualValue(string STRING)
        {

            /// <summary>
            /// Returns searched value from httpd.conf file
            /// </summary>
            
            try
            {
                Class.registryClass rg = new Class.registryClass();
                string dir = rg.getValue("apacheLocation") + "\\conf\\httpd.conf";
                FileStream fin = new FileStream(dir, FileMode.Open);
                StreamReader fstr_in = new StreamReader(fin);
                string data = fstr_in.ReadToEnd();
                fstr_in.Close();
                fin.Close();

                string buf = data.Remove(0, data.IndexOf(STRING));
                data = buf.Remove(data.IndexOf("\n"));
                

                string[] datalines = buf.Split('\n');


                data = datalines[1];

                return data;

                
            }
            catch (Exception ex)
            {
               MessageBox.Show("Błąd: " + ex.Message);
            }
            return "0";
        }

        public void changeValue(string STRING, string changedValue)
        {
            /// <summary>
            /// Changing values
            /// </summary>
           
            Class.registryClass rg = new Class.registryClass();
            string dir = rg.getValue("apacheLocation") + "\\conf\\httpd.conf";
            FileStream fin = new FileStream(dir, FileMode.Open);
            StreamReader fstr_in = new StreamReader(fin);
            string data = fstr_in.ReadToEnd();
            fstr_in.Close();
            fin.Close();

            string buf = data.Replace(STRING,changedValue);
            
            FileStream fout = new FileStream(dir, FileMode.Create);
            StreamWriter fstr_out = new StreamWriter(fout);
            fstr_out.Write(buf);
            fstr_out.Close();
            fout.Close();
        }

    }
}
