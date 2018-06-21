using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class EmailSender
    {
        private  static readonly SmtpClient Client = new SmtpClient(ConfigurationManager.AppSettings["mailServer"])
        {
            Port = int.Parse(ConfigurationManager.AppSettings["mailPort"]),
            EnableSsl = true,
            Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["mailAccount"],
                ConfigurationManager.AppSettings["mailPassword"])
        };

        public static async Task SendAsync(MailMessage message)
        {
            await Client.SendMailAsync(message);
        }

        public static void Send(MailMessage message)
        {
            Client.Send(message);
        }
    }
}
