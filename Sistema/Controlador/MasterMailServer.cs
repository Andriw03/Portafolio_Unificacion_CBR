using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Windows;

namespace Controlador
{
    public class MasterMailServer
    {
        private SmtpClient smtpClient;
        protected string senderMail { get; set; }
        protected string password { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }


        protected void initializeSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(senderMail,password);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;

        }

        public void sendMail(string subject, string body, string recipientMail)
        {
            Usuario usu = new Usuario();
            Cliente cli = new Cliente();
            SysttemSupportMail sys = new SysttemSupportMail();
            //var mailMessage = new MailMessage();
            MailMessage ms = new MailMessage();
            try
            {
                ms.From = new MailAddress(senderMail);
                ms.To.Add("correo_electronico");
                ms.Subject = subject;
                ms.Body = body;
                ms.Priority = MailPriority.Normal;
                smtpClient.Send(ms);

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally{
                ms.Dispose();
                smtpClient.Dispose();

               
            }
           
        }

       
    }
}
