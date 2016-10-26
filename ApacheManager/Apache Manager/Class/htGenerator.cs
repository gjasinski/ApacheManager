using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Apache_Manager.Class
{
    public class htGenerator
    {
        public string path;
        public void addErrorDocument(int errNo, string path)
        {
            FileStream fos=new FileStream(this.path,FileMode.Open);
            StreamReader reader = new StreamReader(fos);
            StreamWriter writer = new StreamWriter(fos);
            string data = reader.ReadToEnd();
            string[] lines = data.Split('\n');
            int counter=0;
            bool fnd = false;
            foreach (string line in lines)
            {
                if (line.Contains("ErrorDocument "+errNo))
                {
                    lines[counter] = "ErrorDocument " + errNo + " " + path;
                    fnd = true;
                    break;
                }
                counter++;
            }
            if (fnd==false)
            {
                writer.WriteLine("ErrorDocument " + errNo + " " + path);
            }
            else
            {                
                fos = new FileStream(this.path, FileMode.Truncate);
                writer = new StreamWriter(fos);
                writer.Write(lines);
            }
            writer.Close();
            writer.Dispose();
            reader.Close();
            reader.Dispose();
            fos.Close();
            fos.Dispose();
        }
        public List<string> showErrorDocuments()
        {
            List<string> lista = new List<string>();
            FileStream fos = new FileStream(this.path, FileMode.Open);
            StreamReader reader = new StreamReader(fos);
            string data = reader.ReadToEnd();
            string[] lines = data.Split('\n');
            int counter = 0;
            foreach (string line in lines)
            {                
                if (line.Contains("ErrorDocument"))
                {
                    lista.Add(lines[counter]);
                    break;
                }
                counter++;
            }
            reader.Close();
            reader.Dispose();
            fos.Close();
            fos.Dispose();
            return lista;
        }
    }
}
