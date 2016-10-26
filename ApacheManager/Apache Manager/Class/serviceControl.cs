using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Apache_Manager.Class
{
    public class serviceControl
    {
        // public class ServiceController : Component;
        public Class.registryClass rc = new registryClass();
        public string serviceName;
        public serviceControl()
        {
            this.serviceName = rc.getValue("serviceName");
        }


        ////////////////start uslugi


        public void StartService()
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Błąd" + ex.Message);
            }
        }
        ////////////////stop uslugi
        public void StopService()
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Błąd" + ex.Message);
            }
        }
        ///////////////////////////////////
        ////////////////restart uslugi
        public void RestartService()
        {
            ServiceController service = new ServiceController(serviceName);
            try{

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch(Exception ex)
            {
               MessageBox.Show("Błąd " + ex.Message);
            }
        }
        ///////////////////////////////////

    }
}