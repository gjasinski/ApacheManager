using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Apache_Manager.Class
{
    class exceptionControl
    {
        private FileStream file;
        private StreamWriter saveFile;
        public registryClass rc=new registryClass();
        private string text;

        public void ZapiszNowy(string path)
        {
            file = new FileStream(path, FileMode.Create, FileAccess.Write);
            saveFile = new StreamWriter(file);
            saveFile.Write(text);
            saveFile.Close();
            file.Close();
        }

        public void errorLog (Exception ex)
        {
            
            string logPath = rc.getValue("ProgramErrorLog");
        }
    }
}
