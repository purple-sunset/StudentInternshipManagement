using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Models.Entities;
using Services.ViewModel;

namespace Services.Interfaces
{
    public interface IUserService
    {
        bool Add(ApplicationUser user, string role);
        Task<bool> AddAsync(ApplicationUser user, string role);
        bool Delete(string id);
        Task<bool> DeleteAsync(string id);
        bool Delete(ApplicationUser user);
        Task<bool> DeleteAsync(ApplicationUser user);
        IQueryable<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        Task<ApplicationUser> GetByIdAsync(string id);
        ApplicationUser GetByUserName(string userName);
        Task<ApplicationUser> GetByUserNameAsync(string userName);
        ApplicationUser GetByEmail(string email);
        Task<ApplicationUser> GetByEmailAsync(string email);
        bool Update(ApplicationUser user);
        Task<bool> UpdateAsync(ApplicationUser user);
        SignInStatus LogIn(LoginViewModel model);
        Task<SignInStatus> LogInAsync(LoginViewModel model);
        void LogOut();
        Task<string> ForgotPasswordAsync(ForgotPasswordViewModel model);
        Task<string> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<string> ChangePasswordAsync(ChangePasswordViewModel model);
    }
}