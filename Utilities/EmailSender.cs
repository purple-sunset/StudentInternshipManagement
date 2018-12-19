using System;
using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Utilities
{
    public static class EmailSender
    {
        private static readonly SmtpClient Client = new SmtpClient(ConfigurationManager.AppSettings["mailServer"])
        {
            Port = int.Parse(ConfigurationManager.AppSettings["mailPort"]),
            EnableSsl = true,
            Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["mailAccount"],
                ConfigurationManager.AppSettings["mailPassword"])
        };

        public static async Task<bool> SendAsync(MailMessage message)
        {
            try
            {
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
