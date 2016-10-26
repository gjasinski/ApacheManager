using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Apache_Manager.Class
{
    public class accessControl
    {
        private Class.registryClass rc = new registryClass();
        public string restrictionFile;
        public void denyAccess_IP(IPAddress ip){
            FileStream fos = new FileStream(this.restrictionFile, FileMode.Open);
            StreamWriter writer = new StreamWriter(fos);
            StreamReader reader = new StreamReader(fos);
            string data = reader.ReadToEnd();
            if(data.Contains("Order Allow")){
                writer.WriteLine("Deny from " + ip.ToString());
            }
            else{
                writer.WriteLine("Order Allow,Deny");
                writer.WriteLine("Allow from all");
                writer.WriteLine("Deny from " + ip.ToString());
            }
            writer.Close();
            writer.Dispose();
            reader.Close();
            reader.Dispose();
            fos.Close();
            fos.Dispose();
        }
        public List<IPAddress> getBlockedIPList()
        {
            FileStream fos = new FileStream(this.restrictionFile, FileMode.Open);
            StreamReader reader = new StreamReader(fos);
            string data = reader.ReadToEnd();
            reader.Close();
            fos.Close();
            data = data.Replace('\n', '|');
            string[] lines = data.Split('|');
            
            List<IPAddress> ips = new List<IPAddress>();
            if (lines.Length != 1)
            {
                for(int i=2; i<lines.Length;i++)
                {
                    string b = lines[i].Replace("Deny from ", String.Empty).Replace("\n",String.Empty).Trim();
                    if (b != String.Empty)
                    {
                        try
                        {
                            ips.Add(IPAddress.Parse(b));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + " ." + b+".");
                        }
                    }
                }
            }
            else
            {
                ips.Add(IPAddress.Parse("0.0.0.0"));
            }
            return ips;
        }
        public void unlockAccess(IPAddress ip)
        {
            FileStream fos = new FileStream(this.restrictionFile, FileMode.Open);            
            StreamReader reader = new StreamReader(fos);
            string data = reader.ReadToEnd();
            if (data.Contains(ip.ToString()))
            {
                data = data.Remove(data.IndexOf(ip.ToString()) - 10, 10 + ip.ToString().Length);
            }
            reader.Close();
            FileStream fos2 = new FileStream(this.restrictionFile, FileMode.Truncate);
            StreamWriter writer = new StreamWriter(fos2);
            writer.Write(data);
            writer.Close();
            reader.Close();
            fos.Close();
        }
    }
}
