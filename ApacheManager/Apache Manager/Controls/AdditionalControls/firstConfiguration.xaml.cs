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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Apache_Manager.Controls.AdditionalControls
{
    /// <summary>
    /// Interaction logic for firstConfiguration.xaml
    /// </summary>
    public partial class firstConfiguration : UserControl
    {
        MainWindow mw;
        private Class.registryClass rc = new Class.registryClass();
        private Class.auth aut = new Class.auth();
        Controls.DirectorySelect.directorySelect apacheSelect = new Controls.DirectorySelect.directorySelect();
        Controls.DirectorySelect.directorySelect logsSelect = new Controls.DirectorySelect.directorySelect();
        Controls.DirectorySelect.directorySelect backupSelect = new Controls.DirectorySelect.directorySelect();

        public firstConfiguration(MainWindow m)
        {
            this.mw = m;
            InitializeComponent();
            this.gridapache.Children.Add(apacheSelect);
            this.gridlogs.Children.Add(logsSelect);
            this.gridbackup.Children.Add(backupSelect);

        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "") MessageBox.Show("Podaj adres e-mail", "Info");
            else
            {
                if (!tbEmail.Text.Contains("@")) MessageBox.Show("Niepoprawny adres email", "Info");
                else
                {
                    if (!tbEmail.Text.Contains(".")) { MessageBox.Show("Niepoprawny adres email", "Info"); }
                    else
                    {
                        if (tbSmtpServer.Text == "") MessageBox.Show("Podaj adress serwera SMTP", "Info");
                        else
                        {
                            if (!tbSmtpServer.Text.Contains(".")) { MessageBox.Show("Niepoprawny adres", "Info"); }
                            else
                            {
                                if (!Directory.Exists(apacheSelect.fileName.Text))
                                {
                                    try
                                    {
                                        Directory.CreateDirectory(apacheSelect.fileName.Text);
                                    }
                                    catch { MessageBox.Show("Niepoprawna lokalizacja Apacha!", "ERROR"); }
                                }
                                if (Directory.Exists(apacheSelect.fileName.Text))
                                {
                                    if (!Directory.Exists(logsSelect.fileName.Text))
                                    {
                                        try
                                        {
                                            Directory.CreateDirectory(logsSelect.fileName.Text);
                                        }
                                        catch { MessageBox.Show("Niepoprawna lokalizacja logów!", "ERROR"); }
                                    }
                                    if (Directory.Exists(logsSelect.fileName.Text))
                                    {
                                        if (!Directory.Exists(backupSelect.fileName.Text))
                                        {
                                            try
                                            {
                                                Directory.CreateDirectory(backupSelect.fileName.Text);
                                            }
                                            catch { MessageBox.Show("Niepoprawna lokalizacja kopi zapasowej!", "ERROR"); }
                                        }
                                        if (Directory.Exists(backupSelect.fileName.Text))
                                        {
                                            if (tbServiceName.Text == "") { MessageBox.Show("Brak Service name!", "ERROR"); }
                                            else
                                            {
                                                if (pbMasterPass.Password.Length < 5)
                                                {
                                                    MessageBox.Show("Kopia zapasowa: Zbyt krótkie hasło!", "ERROR");
                                                }
                                                else
                                                {
                                                    if (pbMasterPass.Password != pbMasterPassCheck.Password) { MessageBox.Show("Ustawienia programu: Podane hasła różnią się!", "ERROR"); }
                                                    else
                                                    {
                                                        rc.setValue("SO_email", tbEmail.Text);
                                                        rc.setValue("backupLocation", backupSelect.fileName.Text);
                                                        rc.setValue("auditSMTP", tbSmtpServer.Text);
                                                        rc.setValue("apacheLocation", apacheSelect.fileName.Text);
                                                        rc.setValue("logLocation", logsSelect.fileName.Text);
                                                        rc.setValue("serviceName", tbServiceName.Text);
                                                        aut.registerAccess("masterAccess", pbMasterPass.Password);
                                                        rc.setValue("Configured", "true"); //ustawia klucz na cokolwiek, oznacza to że jest skonfigurowane
                                                        mw.Content.Children.Clear();
                                                        //mw.Content.Children.Add(new COS TAM PO ZAKONCZENIU KONFIGURACJI);
                                                        MessageBox.Show("Konfiguracja została zmieniona.", "INFO");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}
