using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Apache_Manager.Class
{
    class moduleManager
    {
        /// <summary>
        /// Adding starting and ending label to httpd.conf
        /// </summary>
        /// <returns></returns>
        public bool configureModules()
        {
            try
            {
                Class.registryClass rg = new Class.registryClass();
                string dir = rg.getValue("apacheLocation") + "\\conf\\httpd.conf";
                FileStream fin = new FileStream(dir, FileMode.Open);
                StreamReader fstr_in = new StreamReader(fin);
                string data = fstr_in.ReadToEnd();
                fstr_in.Close();
                fin.Close();
                int firstmodule = 0;
                if(data.IndexOf("\nLoadModule")>data.IndexOf("\n#LoadModule")) firstmodule = data.IndexOf("\n#LoadModule"); else firstmodule = data.IndexOf("\nLoadModule");
                data = data.Insert(firstmodule-1,"\n\n\n#HERE STARTS CONFIGURATION MODULES, DON'T DELETE THIS!");
                string[] datalines = data.Split('\n');
                int secmodule = 0;
                foreach (string line in datalines)
                {
                    if (line.Contains("#LoadModule "))
                    {
                        secmodule = data.IndexOf(line)+line.Length;
                    }
                    if (line.Contains("LoadModule "))
                    {
                        firstmodule = data.IndexOf(line)+line.Length;
                    }
                }
                if (firstmodule < secmodule) firstmodule = secmodule;
                data = data.Insert(firstmodule, "\n#HERE ENDS CONFIGURATION MODULES, DON'T DELETE THIS!\n\n");
                FileStream fout = new FileStream(dir, FileMode.Truncate);
                StreamWriter fstr_out = new StreamWriter(fout);
                fstr_out.Write(data);
                fout.Close();
                fstr_out.Close();     
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return false;
            }
        }

        /// <summary>
        /// Checking configuration file and preparing it
        /// </summary>
        /// <returns></returns>
        public bool checkConfigureModules() {
            try
            {
                Class.registryClass rg = new Class.registryClass();
                string dir = rg.getValue("apacheLocation") + "\\conf\\httpd.conf";
                FileStream fin = new FileStream(dir, FileMode.Open);
                StreamReader fstr_in = new StreamReader(fin);
                
                string data = fstr_in.ReadToEnd();
                fstr_in.Close();
                fin.Close();
                if (data.Contains("#HERE STARTS CONFIGURATION MODULES, DON'T DELETE THIS!"))
                {
                    return true;
                }
                else
                {
                    if (configureModules()) return true; else return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR"); 
                return false;
            }
        }
        /// <summary>
        /// Return dictionary of names and directory of modules
        /// </summary>
        /// <param name="searchrule">Shows: 0 - all modules, 1 - deactive modules, 2 - active modules</param>
        /// <returns>Key - module name, value - module directory</returns>
        public SortedDictionary<string,string> returnModuleList(int searchrule)
        {
            SortedDictionary<string,string> modules = new SortedDictionary<string,string>();
            try
            {
                Class.registryClass rg = new Class.registryClass();
                string dir = rg.getValue("apacheLocation") + "\\conf\\httpd.conf";
                FileStream fin = new FileStream(dir, FileMode.Open);
                StreamReader fstr_in = new StreamReader(fin);
                string data = fstr_in.ReadToEnd();
                int firstmodule = data.IndexOf("LoadModule ");
                data = data.Remove(0, firstmodule - 2);
                string[] datalines = data.Split('\n');
                string buf;
                bool check = false;
                switch (searchrule)
                {
                    case 0:
                        foreach (string line in datalines)
                        {
                            try
                            {
                                if (line.Contains("#HERE STARTS CONFIGURATION MODULES, DON'T DELETE THIS!")) check = true;
                                if (check)
                                {
                                    if (line.Contains("LoadModule ") && (!line.Contains("# LoadModule")))
                                    {
                                        buf = line.Remove(0, line.IndexOf("e ") + 2);
                                        modules.Add(buf.Remove(buf.IndexOf(" ")), buf.Remove(0, buf.IndexOf(" ") + 1));
                                    }
                                }
                                if (line.Contains("#HERE ENDS CONFIGURATION MODULES, DON'T DELETE THIS!")) break;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Błąd : " + e.Message, "Błąd",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        break;
                    case 1:
                        foreach (string line in datalines)
                        {
                            try
                            {
                                if (line.Contains("#HERE STARTS CONFIGURATION MODULES, DON'T DELETE THIS!")) check = true;
                                if (check)
                                {
                                    if (line.Contains("#LoadModule "))
                                    {
                                        buf = line.Remove(0, line.IndexOf("e ") + 2);
                                        modules.Add(buf.Remove(buf.IndexOf(" ")), buf.Remove(0, buf.IndexOf(" ") + 1));
                                    }
                                }
                                if (line.Contains("#HERE ENDS CONFIGURATION MODULES, DON'T DELETE THIS!")) break;
                            }
                            catch (Exception e) 
                            {
                                MessageBox.Show("Błąd : " + e.Message, "Błąd",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        break;
                    case 2:
                        foreach (string line in datalines)
                        {
                            try
                            {
                                if (line.Contains("#HERE STARTS CONFIGURATION MODULES, DON'T DELETE THIS!")) check = true;
                                if (check)
                                {
                                    if (line[0] == 'L' && line.Contains("LoadModule "))
                                    {
                                        buf = line.Remove(0, line.IndexOf("e ") + 2);
                                        modules.Add(buf.Remove(buf.IndexOf(" ")), buf.Remove(0, buf.IndexOf(" ") + 1));
                                    }
                                }
                                if (line.Contains("#HERE ENDS CONFIGURATION MODULES, DON'T DELETE THIS!")) break;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Błąd : " + e.Message, "Błąd",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        break;
                }
                fstr_in.Close();
                fin.Close();
                return modules;
            }
            catch (Exception e)
            {
                MessageBox.Show("BŁĄD: " + e.ToString(), "ERROR");
                return modules;
            }
        }

        /// <summary>
        /// Acctive module
        /// </summary>
        /// <param name="moduleName">Module name in httpd.conf</param>
        /// <returns></returns>
        public bool activeModule(string moduleName)
        {
            try{
            Class.registryClass rg = new Class.registryClass();
            string dir = rg.getValue("apacheLocation") + "\\conf\\httpd.conf";
            FileStream fin = new FileStream(dir, FileMode.Open);
            StreamReader fstr_in = new StreamReader(fin);
            string data = fstr_in.ReadToEnd();
            fstr_in.Close();
            fin.Close();
            int indexofname = data.IndexOf(" "+moduleName+" ");
            if (indexofname > data.IndexOf("#HERE ENDS CONFIGURATION MODULES, DON'T DELETE THIS!") || indexofname < data.IndexOf("#HERE STARTS CONFIGURATION MODULES, DON'T DELETE THIS!")) return false;
            string buf = data.Remove(indexofname - 11, 1);
            FileStream fout = new FileStream(dir, FileMode.Create);
            StreamWriter fstr_out = new StreamWriter(fout);
            fstr_out.Write(buf);
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
        /// Deactive module in httpd.conf
        /// </summary>
        /// <param name="moduleName">Module name in httpd.conf</param>
        /// <returns></returns>
        public bool deactiveModule(string moduleName)
        {
            try
            {
                Class.registryClass rg = new Class.registryClass();
                string dir = rg.getValue("apacheLocation") + "\\conf\\httpd.conf";
                FileStream fin = new FileStream(dir, FileMode.Open);
                StreamReader fstr_in = new StreamReader(fin);
                string data = fstr_in.ReadToEnd();
                fstr_in.Close();
                fin.Close();
                int indexofname = data.IndexOf(" "+moduleName+" ");
                string buf = data.Insert(indexofname - 10, "#");
                FileStream fout = new FileStream(dir, FileMode.Create);
                StreamWriter fstr_out = new StreamWriter(fout);
                fstr_out.Write(buf);
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
        /// Deleting module from configuration file and module file
        /// </summary>
        /// <param name="moduleName">Name of module from httpd.conf</param>
        /// <param name="modulelocation">Location of module from httpd.conf</param>
        /// <returns></returns>
        public bool deleteModule(string moduleName, string modulelocation)
        {
            try
            {
                Class.registryClass rg = new Class.registryClass();
                string apachelocation = rg.getValue("apacheLocation");
                string httpdlocation = apachelocation + "\\conf\\httpd.conf";
                FileStream fin = new FileStream(httpdlocation, FileMode.Open);
                StreamReader fstr_in = new StreamReader(fin);
                string data = fstr_in.ReadToEnd();
                fstr_in.Close();
                fin.Close();
                int indexofname;
                int lngth;
                string newdata;
                if (data.Contains("#LoadModule " + moduleName))
                {
                    indexofname = data.IndexOf("#LoadModule " + moduleName);
                    lngth = data.IndexOf(modulelocation) + modulelocation.Length - indexofname;
                    newdata = data.Remove(indexofname, lngth + 1);
                }
                else
                {
                    indexofname = data.IndexOf("LoadModule " + moduleName);
                    lngth = data.IndexOf(modulelocation) + modulelocation.Length - indexofname;
                    newdata = data.Remove(indexofname, lngth + 1);
                }
                if (modulelocation.Contains('"'))
                {
                    modulelocation = modulelocation.Remove(modulelocation.IndexOf("\""), 1);
                    modulelocation = modulelocation.Remove(modulelocation.IndexOf("\""), 1);
                    File.Delete(modulelocation);
                }
                else
                {
                    modulelocation = modulelocation.Remove(0, modulelocation.IndexOf('/'));
                    File.Delete(apachelocation + "\\modules\\" + modulelocation);
                }

                FileStream fout = new FileStream(httpdlocation, FileMode.Truncate);
                StreamWriter fstr_out = new StreamWriter(fout);
                fstr_out.Write(newdata);
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
        /// Adding module to configuration file(default deactive), and copy module file to module location
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="moduleLocation"></param>
        /// <returns></returns>
        public string addModule(string moduleName, string moduleLocation)
        {
            try
            {
                Class.registryClass rg = new Class.registryClass();
                string apachelocation = rg.getValue("apacheLocation");
                string filename = moduleLocation;
                while (filename.Contains('\\'))
                {
                    filename = filename.Remove(0, filename.IndexOf('\\') + 1);
                }
                filename = "modules/" + filename;
                string apachemodulelocation = apachelocation + "/"+filename;
                if (File.Exists(apachemodulelocation)) { return "Nie można dodać. Ten plik istnieje."; }
                else
                {
                    File.Copy(moduleLocation, apachemodulelocation); // w tym miejscu dokonano skopiowania krok 1
                }

                SortedDictionary<string, string> modules = returnModuleList(0);
                modules.Add(moduleName, filename);       //w tym miejscu dodano do słownika nazwe modułu i lokalizacje

                FileStream fin = new FileStream(apachelocation + "/conf/httpd.conf", FileMode.Open);
                StreamReader fstr_in = new StreamReader(fin);
                string data = fstr_in.ReadToEnd();
                fstr_in.Close();
                fin.Close();
                int start = data.IndexOf("#HERE STARTS CONFIGURATION MODULES, DON'T DELETE THIS!") + 55;
                int stop = data.IndexOf("#HERE ENDS CONFIGURATION MODULES, DON'T DELETE THIS!") - start;
                data = data.Remove(start, stop);
                start = data.IndexOf("#HERE STARTS CONFIGURATION MODULES, DON'T DELETE THIS!") + 55;
                string line = "";
                data = data.Insert(start, "\n");
                foreach (KeyValuePair<string, string> pair in modules)
                {
                    line = "#LoadModule " + pair.Key + " " + pair.Value + "\n";
                    data = data.Insert(start, line);
                    start += line.Length;
                }

                FileStream fout = new FileStream(apachelocation + "\\conf\\httpd.conf", FileMode.Truncate);
                StreamWriter fstr_out = new StreamWriter(fout);
                fstr_out.Write(data);
                fstr_out.Close();
                fout.Close();
                return "true";
            }
            catch(Exception e)
            { return e.ToString(); }          
        }
    }
}
