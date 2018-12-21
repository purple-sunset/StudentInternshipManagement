using System.Threading.Tasks;
using Services.ViewModel;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        void SendCreateEmail();
        void SendProcessEmail();
        Task SendResetPasswordMailAsync(ResetPasswordViewModel model);
    }
}