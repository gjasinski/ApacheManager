using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;

namespace Apache_Manager.Class
{
    /// <summary>
    /// Class.registryClass
    /// </summary>
    public class registryClass
    {
        public RegistryKey regKey;
        /// <summary>
        /// Registry access class. Base key is HKEY_CURRENT_USER. Registry Mode 32
        /// </summary>
        public registryClass()
        {
            regKey= RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
        }
        /// <summary>
        /// Gets value of registry key
        /// </summary>
        /// <param name="KeyName">Name of key</param>
        /// <returns>Returns value of key</returns>
        public string getValue(string KeyName)
        {
            RegistryKey rk = regKey;
            RegistryKey sk1 = rk.OpenSubKey("ApacheManager");
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd : " + ex.Message, "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
        }
        /// <summary>
        /// Create new key in registry
        /// </summary>
        /// <param name="KeyName">Name of key to be created</param>
        /// <param name="Value">Value of key</param>
        /// <returns>True on success, false on setback</returns>
        public bool setValue(string KeyName, object Value)
        {
            try
            {
                RegistryKey rk = regKey;
                RegistryKey sk1 = rk.CreateSubKey("ApacheManager");
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd : " + ex.Message, "Błąd",
                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        /// <summary>
        /// Checks whether application is configured (key Configured is set true)
        /// </summary>
        /// <returns>True on success, false on setback</returns>
        public bool checkConfiguration()
        {
            RegistryKey rk = regKey;
            RegistryKey sk1 = rk.CreateSubKey("ApacheManager");
            if ((string)sk1.GetValue("Configured") == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Deletes key (with subkeys) from registry
        /// </summary>
        /// <param name="keyName">Name of key to be deleted</param>
        /// <returns>True on success, false on setback</returns>
        public bool deleteKey(string keyName)
        {
            try
            {
                RegistryKey rk = regKey;
                RegistryKey sk1 = rk.CreateSubKey("ApacheManager");
                sk1.DeleteSubKey(keyName.ToUpper());
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd : " + ex.Message, "Błąd",
                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        /// <summary>
        /// Checks if key exists
        /// </summary>
        /// <param name="keyName">Name of key to be checked</param>
        /// <returns>True on success, false on setback</returns>
        public bool checkKey(string keyName){
            try
            {
                RegistryKey rk = regKey.OpenSubKey("ApacheManager");
                rk.OpenSubKey(keyName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd : " + ex.Message, "Błąd",
                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
