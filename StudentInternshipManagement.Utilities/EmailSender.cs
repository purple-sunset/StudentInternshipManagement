using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StudentInternshipManagement.Utilities
{
    public static class EmailSender
    {
        private static readonly SmtpClient Client = new SmtpClient(ConfigurationManager.AppSettings["mailServer"])
        {
            Port = int.Parse(ConfigurationManager.AppSettings["mailPort"]),
            EnableSsl = true,
            Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["mailAccount"],
                ConfigurationManager.AppSettings["mailPassword"])
        };

        private static readonly MailAddress From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);

        public static async Task<bool> SendAsync(MailMessage message)
        {
            try
            {
                message.From = From;
                await Client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public static bool Send(MailMessage message)
        {
            try
            {
                message.From = From;
                Client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }
    }
}