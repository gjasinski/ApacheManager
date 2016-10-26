using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;

namespace Apache_Manager.Class
{
    /// <summary>
    /// ApacheManager.Class.mail
    /// </summary>
    class mail
    {
        private string sender;
        SmtpClient client;
        MailMessage message = new MailMessage();
        MailAddress address;
        NetworkCredential basicCredential = new NetworkCredential();
        Class.registryClass rc = new registryClass();
        /// <summary>
        /// Class.mail
        /// </summary>
        /// <param name="s">Sender's email address</param>
        /// <param name="uname">Username (SMTP server login)</param>
        /// <param name="passwd">Password (SMTP)</param>
        public mail()
        {
            string uname = rc.getValue("emailUname");
            string passwd = rc.getValue("emailPassword");
            string s = rc.getValue("email");
            this.client = new SmtpClient(rc.getValue("auditSMTP"));
            this.sender = s;
            this.address = new MailAddress(s);
            this.client.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.basicCredential.UserName = uname;
            this.basicCredential.Password = passwd;
            this.client.UseDefaultCredentials = false;
            this.client.Credentials = this.basicCredential;
            this.client.EnableSsl = true;
            this.client.Port = 587;
            message.From = address;
        }
        /// <summary>
        /// Send email to server operator
        /// </summary>
        /// <param name="type">Type of predefined message</param>
        /// <param name="param">Additional parameter</param>
        public void sendMail(string type,string param)
        {
            switch (type)
            {
                case "AUDIT_SECURITY_VIOLATION":
                    this.message.Subject = "Audyt bezpieczeństwa programu ApacheManager";
                    LinkedResource logo = new LinkedResource("Graphics/security_audit_logo.png",MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "logo";
                    LinkedResource foot = new LinkedResource("Graphics/email_foot.png",MediaTypeNames.Image.Jpeg);
                    foot.ContentId = "foot";
                    AlternateView av1 = AlternateView.CreateAlternateViewFromString("<img src=\"cid:logo\"/></br><h4>------------Wiadomość automatyczna------------</h4></br><h5>" + DateTime.Now + " : nastąpiło 3-krotne nieudane logowanie do konsoli zarządzania (obiekt " + param + ")</h5></br></br></br></br></br></br></br><h5>W przypadku gdy nie był to operator serwera mogło dojść do naruszenia bezpieczeństwa systemu.<div width=\"1000px\" heigth=\"50px\" bgcolor=\"#252525\"><img src=\"cid:foot\"/></div></br>", null, MediaTypeNames.Text.Html);
                    av1.LinkedResources.Add(logo);
                    av1.LinkedResources.Add(foot);
                    this.message.AlternateViews.Add(av1);
                    break;
            }
            message.IsBodyHtml = true;
            message.To.Add(new MailAddress(rc.getValue("SO_email"))); // server operator's email.
            client.Send(message);
        }
    }
}
