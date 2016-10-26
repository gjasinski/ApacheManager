using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Apache_Manager.Windows
{
    /// <summary>
    /// Interaction logic for windowOptions.xaml
    /// </summary>
    public partial class windowOptions : Window
    {
        Class.registryClass rc = new Class.registryClass();
        Class.auth auth = new Class.auth();
        public MainWindow parent;
        private Controls.DirectorySelect.directorySelect selectAL = new Controls.DirectorySelect.directorySelect();
        private Controls.DirectorySelect.directorySelect selectBL = new Controls.DirectorySelect.directorySelect();
        private Controls.DirectorySelect.directorySelect selectLL = new Controls.DirectorySelect.directorySelect();
        public windowOptions(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.DirectorySelect_AL.Children.Add(selectAL);
            this.DirectorySelect_BL.Children.Add(selectBL);
            this.DirectorySelect_LL.Children.Add(selectLL);
            if (this.rc.getValue("APACHELOCATION") != String.Empty)
            {
                this.selectAL.fileName.Text = this.rc.getValue("APACHELOCATION");
            }
            if (this.rc.getValue("BACKUPLOCATION") != String.Empty)
            {
                this.selectBL.fileName.Text = this.rc.getValue("BACKUPLOCATION");
            }
            if (this.rc.getValue("LOGLOCATION") != String.Empty)
            {
                this.selectLL.fileName.Text = this.rc.getValue("LOGLOCATION");
            }
            if (this.rc.getValue("SO_EMAIL") != String.Empty)
            {
                this.SO_Email.Text = this.rc.getValue("SO_EMAIL");
            }
            if (this.rc.getValue("AUDITSMTP") != String.Empty)
            {
                this.SMTP_Address.Text = this.rc.getValue("AUDITSMTP");
            }
            if (this.rc.getValue("emailUname") != String.Empty)
            {
                this.SMTP_Username.Text = this.rc.getValue("emailUname");
            }
            if (this.rc.getValue("email") != String.Empty)
            {
                this.Server_Email.Text = this.rc.getValue("email");
            }
            if (this.rc.getValue("SERVICENAME") != String.Empty)
            {
                this.serviceName.Text = this.rc.getValue("SERVICENAME");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            parent.nodeExists = false;
        }

        private void btnSaveLocations_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectAL.fileName.Text != this.rc.getValue("APACHELOCATION") || this.selectAL.fileName.Text!=String.Empty)
            {
                if (this.selectAL.fileName.Text.Trim() != string.Empty && this.selectAL.fileName.Text.Length > 0 && Directory.Exists(this.selectAL.fileName.Text))
                {
                    this.rc.setValue("APACHELOCATION", this.selectAL.fileName.Text);
                }
                else
                {
                    MessageBox.Show("Sprawdź poprawność wprowadzonych danych dla ApacheLocation! Upewnij się, że folder istnieje na dysku", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            if (this.selectBL.fileName.Text != this.rc.getValue("BACKUPLOCATION") || this.selectBL.fileName.Text != String.Empty)
            {
                if (this.selectBL.fileName.Text.Trim() != string.Empty && this.selectBL.fileName.Text.Length > 0 && Directory.Exists(this.selectAL.fileName.Text))
                {
                    this.rc.setValue("BACKUPLOCATION", this.selectBL.fileName.Text);
                }
                else
                {
                    MessageBox.Show("Sprawdź poprawność wprowadzonych danych dla BackupLocation! Upewnij się, że folder istnieje na dysku", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            if (this.selectLL.fileName.Text != this.rc.getValue("LOGLOCATION") || this.selectLL.fileName.Text != String.Empty)
            {
                if (this.selectLL.fileName.Text.Trim() != string.Empty && this.selectLL.fileName.Text.Length > 0 && Directory.Exists(this.selectAL.fileName.Text))
                {
                    this.rc.setValue("LOGLOCATION", this.selectLL.fileName.Text);
                }
                else
                {
                    MessageBox.Show("Sprawdź poprawność wprowadzonych danych dla LogLocation! Upewnij się, że folder istnieje na dysku", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnSavePasswords_Click(object sender, RoutedEventArgs e)
        {
            if (this.auth.authorise("MASTERACCESS", this.CurrentPassword.Password) || this.CurrentPassword.Password != String.Empty)
            {
                if(this.NewPassword.Password == this.RetypePassword.Password){
                    this.auth.registerAccess("MASTERACCESS", this.NewPassword.Password);//, this.CurrentPassword.Password))                    
                        parent.auth.killSession();
                        MessageBox.Show("Hasło zostało zmienione pomyślnie", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else{
                    MessageBox.Show("Podane hasła nie pasują do siebie!","Błąd",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else{
                MessageBox.Show("Podane hasło jest nieprawidłowe!","Błąd",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkSave.IsChecked==true)
            {
                this.rc.setValue("Configured", "true");
            }
            if (this.SO_Email.Text != this.rc.getValue("SO_EMAIL") || this.SO_Email.Text != String.Empty)
            {
                if (this.SO_Email.Text.Trim() != String.Empty && this.SO_Email.Text.Length > 0 && this.SO_Email.Text.Contains("@"))
                {
                    rc.setValue("SO_EMAIL", this.SO_Email.Text);
                }
                else
                {
                    MessageBox.Show("Niepoprawny format adresu email! Adres musi mieć formę : example@corp.domain ", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (this.SMTP_Address.Text != this.rc.getValue("AUDITSMTP") || this.SMTP_Address.Text != String.Empty)
            {
                if (this.SMTP_Address.Text.Trim() != String.Empty && this.SMTP_Address.Text.Contains(".") && this.SMTP_Address.Text.Length > 0)
                {
                    this.rc.setValue("AUDITSMTP", this.SMTP_Address.Text);
                }
                else
                {
                    MessageBox.Show("Niepoprawny format adresu serwera SMTP!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (this.Server_Email.Text != this.rc.getValue("EMAIL") || this.Server_Email.Text!=String.Empty)
            {
                if (this.Server_Email.Text.Trim() != String.Empty && this.Server_Email.Text.Length > 0 && this.Server_Email.Text.Contains("@"))
                {
                    rc.setValue("EMAIL", this.Server_Email.Text);
                }
                else
                {
                    MessageBox.Show("Niepoprawny format adresu email! Adres musi mieć formę : example@corp.domain ", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (this.SMTP_Username.Text != rc.getValue("emailUname") || this.SMTP_Username.Text!=String.Empty)
            {
                if (this.SMTP_Username.Text.Trim() != String.Empty && this.SMTP_Username.Text.Length > 0)
                {
                    rc.setValue("emailUname", this.SMTP_Username.Text);
                }
                else
                {
                    MessageBox.Show("Podaj nazwę użytkownika SMTP!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (this.SMTP_Password.Password != rc.getValue("emailPassword") && this.SMTP_Password.Password != String.Empty)
            {
                if (this.SMTP_Password.Password.Trim() != String.Empty && this.SMTP_Password.Password.Length > 0)
                {
                    rc.setValue("emailPassword", this.SMTP_Password.Password);
                }
                else
                {
                    MessageBox.Show("Podaj hasło użytkownika SMTP!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (this.serviceName.Text != this.rc.getValue("SERVICENAME") || this.serviceName.Text != String.Empty)
            {
                if (this.serviceName.Text.Trim() != String.Empty && this.serviceName.Text.Length > 0)
                {
                    rc.setValue("SERVICENAME", this.serviceName.Text);
                }
                else
                {
                    MessageBox.Show("Podaj nazwę usługi Apache", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
