using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Apache_Manager.Class
{
    class virtualHostManager
    {
        /// <summary>
        /// Return extra/httpd-vhosts.conf
        /// </summary>
        /// <returns></returns>
        public string vhread()
        {
            try
            {
                Class.registryClass rg = new Class.registryClass();
                string apachelocation = rg.getValue("apacheLocation");
                string dir = apachelocation + "\\conf\\extra\\httpd-vhosts.conf";
                FileStream fin = new FileStream(dir, FileMode.Open);
                StreamReader fstr_in = new StreamReader(fin);
                string data = fstr_in.ReadToEnd();
                fstr_in.Close();
                fin.Close();
                return data;
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return null;
            }
        }

        /// <summary>
        /// Rewrite extra\\httpd-vhosts.conf
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Configuration file content</returns>
        public bool vhwrite(string data)
        {
            try
            {
                Class.registryClass rg = new Class.registryClass();
                string apachelocation = rg.getValue("apacheLocation");
                string dir = apachelocation + "\\conf\\extra\\httpd-vhosts.conf";
                FileStream fout = new FileStream(dir, FileMode.Truncate);
                StreamWriter fstr_out = new StreamWriter(fout);
                fstr_out.Write(data);
                fstr_out.Close();
                fout.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return false;
            }
        }

        /// <summary>
        /// Returs list of virtualhosts name
        /// </summary>
        /// <returns>Returns list of strings</returns>
        public List<string> vhnames()
        {
            List<string> vhostnames = new List<string>();
            try
            {
                string data = vhread();
                if (data.Contains("<VirtualHost "))
                {
                    data = data.Remove(0, data.IndexOf("<VirtualHost "));
                    string[] vhost = data.Split('\n');
                    string buf;
                    foreach (string line in vhost)
                    {
                        if (line.Contains("ServerName"))
                        {
                            buf = line.Remove(0, line.IndexOf("e ") + 2);
                            //buf = buf.Remove(buf.Length);
                            vhostnames.Add(buf);
                        }
                    }
                    return vhostnames;
                }
                else
                {
                    vhostnames.Add("Brak Virtual-Hostów");
                    return vhostnames;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return vhostnames;
            }
        }

        /// <summary>
        /// Adding virtualhost to extra/httpd-vhosts.conf
        /// </summary>
        /// <param name="email">Admin email</param>
        /// <param name="indexLocation">Main file location</param>
        /// <param name="serverName">Serwer name, must be unique</param>
        /// <param name="port">Port</param>
        /// <returns></returns>
        public bool vhadd(string email, string indexLocation, string serverName, string ip, int port)
        {
            try
            {
                string data = vhread();
                if (data.Contains(serverName)) { return false; }
                else
                {
                    indexLocation = indexLocation.Replace('\\', '/');
                    serverName = serverName.Replace(' ', '_');
                    string elog = "\"log/" + serverName + "-error.log\"";
                    string dlog = "\"logs/" + serverName + "-access.log\"";
                    string newVirtualHost = "\n<VirtualHost "+ip+":" + port.ToString() + ">\n    ServerAdmin " + email + "\n    DocumentRoot \"" + indexLocation + "\"\n    ServerName " + serverName + "\n    ErrorLog " + elog + "\n    CustomLog " + dlog + "\n</VirtualHost>\n";
                    data = data.Insert(data.Length - 1, newVirtualHost);
                    if (vhwrite(data)) return true; else return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return false;
            }
        }

        /// <summary>
        /// Delete virtual host
        /// </summary>
        /// <param name="servername">Server name</param>
        /// <returns></returns>
        public bool vhdelete(string servername)
        {
            try
            {
                string data = vhread();
                string headline = data.Remove(data.IndexOf("<VirtualHost "));
                data = data.Remove(0, data.IndexOf("<VirtualHost "));
                string[] lines = data.Split('\n');
                List<string> vhs = new List<string>();
                vhs.Add("");
                int i = 0;
                bool check = false;
                foreach (string ln in lines)
                {
                    vhs[i] += ln + '\n';
                    if (ln.Contains("ServerName " + servername)) check = true;
                    if (ln.Contains("</VirtualHost>"))
                    {
                        if (!check)
                        {
                            i++;
                            vhs.Add("");
                        }
                        else
                        {
                            check = false;
                            vhs[i] = "";
                        }
                    }
                }
                data = headline;
                foreach (string l in vhs)
                {
                    if (!vhs.Contains(servername)) data += l;
                }
                if (vhwrite(data)) return true; else return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return false;
            }
        }
        /// <summary>
        /// Get data to edit control
        /// </summary>
        /// <param name="servername">Server name</param>
        /// <returns>Index: 0 - admin's adress email, 1 - root document directory, 2 - virtualhost name, 3 - port</returns>
        public string[] vhgetdata(string servername)
        {

            string data = vhread();
            string[] lines = data.Split('\n');
            string[] datatoshow = new string[5];
            try
            {
                bool check = false;
                string buf = "";
                foreach (string ln in lines)
                {
                    buf += ln;
                    if (ln.Contains("ServerName " + servername)) check = true;
                    if (ln.Contains("</VirtualHost>"))
                    {
                        if (!check)
                        {
                            buf = "";
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                buf = buf.Remove(0, buf.IndexOf("<VirtualHost"));
                datatoshow[3] = buf.Substring(buf.IndexOf(' ') + 1, buf.IndexOf(':') - buf.IndexOf(' ')-1);
                datatoshow[4] = buf.Substring(buf.IndexOf(':') + 1, buf.IndexOf("ServerAdmin") - buf.IndexOf(':') - 6);
                buf = buf.Remove(0, buf.IndexOf("ServerAdmin"));
                datatoshow[0] = buf.Substring(buf.IndexOf("ServerAdmin ") + 12, buf.IndexOf("DocumentRoot") - buf.IndexOf("ServerAdmin ") - 16);
                buf = buf.Remove(0, buf.IndexOf("DocumentRoot"));
                datatoshow[1] = buf.Substring(buf.IndexOf("DocumentRoot \"") + 14, buf.IndexOf("ServerName") - buf.IndexOf("DocumentRoot \"") - 19);
                buf = buf.Remove(0, buf.IndexOf("ServerName"));
                datatoshow[2] = buf.Substring(buf.IndexOf("ServerName ") + 11, buf.IndexOf("ErrorLog") - buf.IndexOf("ServerName ") - 15);
                return datatoshow;
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return datatoshow;
            }
        }
    }
}
