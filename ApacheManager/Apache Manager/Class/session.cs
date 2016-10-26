using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apache_Manager.Class
{
    public class session
    {
        private bool sessionSet = false;
        private bool logAccess; //1
        private bool backupAccess;//2
        private bool masterAccess;//3
        private Dictionary<string, string> sessionVariables = new Dictionary<string, string>();

        public bool checkSession()
        {
            if (this.sessionSet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setAccess(byte id)
        {
            this.sessionSet = true;
            switch (id)
            {
                case 1:
                    this.logAccess = true;
                    break;
                case 2:
                    this.backupAccess = true;
                    break;
                case 3:
                    this.masterAccess = true;
                    break;
                default:
                    return;
            }
        }

        public bool checkAccess(byte id)
        {
            switch (id)
            {
                case 1:
                    return this.logAccess;
                case 2:
                    return this.backupAccess;
                case 3:
                    return this.masterAccess;
                default:
                    return false;
            }
        }

        public bool registerVariable(string name, string value)
        {
            try
            {
                this.sessionVariables.Add(name, value);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Błąd : " + e.Message, "Błąd");
                return false;
            }
        }

        public string readVariable(string name)
        {
            try
            {
                return this.sessionVariables[name];
            }
            catch (Exception e)
            {
                MessageBox.Show("Błąd : " + e.Message, "Błąd");
                return String.Empty;
            }
        }

        public bool killSession()
        {
            try
            {
                this.sessionSet = false;
                this.sessionVariables.Clear();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd : " + ex.Message, "Błąd");
                return false;
            }
        }
    }
}
