using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apache_Manager.Class
{
    public class auth : Class.session
    {
        private registryClass rc=new registryClass();
        private string registeredHash;
        private short loginCount = 0;
        /// <summary>
        /// Checks if password provided by user matches the password stored in system's registry. If user will provide wrong password 3 times, security audit service will send email to server's operator
        /// </summary>
        /// <param name="accessString">
        /// Access passphrase (password) for selected category
        /// </param>
        /// <param name="type">
        /// m - masterAccess
        /// b - backupAccess
        /// l - logAccess
        /// </param>
        /// <returns>True if authorised, false if not</returns>
        public bool authorise(string accessName, string accessString)
        {
            string accessStringHash = this.calculateSHA512(accessString);
            this.registeredHash = rc.getValue(accessName);
            if (this.registeredHash == accessStringHash) // compare value stored in registry with provided value's hash
            {
                switch (accessName)
                {
                    case "logAccess":
                        setAccess(1);
                        break;
                    case "backupAccess":
                        setAccess(2);
                        break;
                    case "masterAccess":
                        setAccess(3);
                        break;
                }
                return true;
            }
            else
            {
                this.loginCount++;
                if (this.loginCount == 3)
                {
                    Class.mail mail = new Class.mail();
                    mail.sendMail("AUDIT_SECURITY_VIOLATION",accessName);
                    this.loginCount = 0;
                }
                return false;
            }
        }
        /// <summary>
        /// Creates new access object in registry. 
        /// </summary>
        /// <param name="accessName">Name of access object that will be created</param>
        /// <param name="passphrase">Password to access the object</param>
        public void registerAccess(string accessName, string passphrase)
        {
            string accessStringHash = this.calculateSHA512(passphrase); // calculate hash
            this.rc.setValue(accessName, accessStringHash);
        }
        /// <summary>
        /// Changes password for existing access object.
        /// </summary>
        /// <param name="accessName"> Name of object</param>
        /// <param name="newPassphrase">New password</param>
        /// <param name="oldPassphrase">Old password</param>
        /// <returns>True on success, false on setback</returns>
        public bool changePassword(string accessName, string newPassphrase, string oldPassphrase)
        {
            if (this.authorise(oldPassphrase, accessName) == true)
            {
                //rc.deleteKey(accessName); // delete key
                this.rc.setValue(accessName, this.calculateSHA512(newPassphrase)); // create new key with same value
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Calculates SHA512 hash value
        /// </summary>
        /// <param name="entry">Value to be hashed</param>
        /// <returns>String containing calculated hash</returns>
        public string calculateSHA512(string entry)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(entry); // convert entry into byte array
            string accessStringHash = Encoding.ASCII.GetString(System.Security.Cryptography.SHA512Cng.Create("SHA512").ComputeHash(buffer)); //calculate hash of array
            return accessStringHash;
        }
    }
}
