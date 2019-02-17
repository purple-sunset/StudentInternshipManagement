using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentInternshipManagement.Models.Constants;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;
using StudentInternshipManagement.Services.ViewModel;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

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

    #endregion

    #region Class

    public class UserService : IUserService
    {
        //private const string XsrfKey = "XsrfId";
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationUserManager _userManager;

        public UserService(IUnitOfWork unitOfWork, ApplicationUserManager userManager,
            ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _emailService = emailService;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return _unitOfWork.UserRepository().TableNoTracking;
        }

        public ApplicationUser GetById(string id)
        {
            return _unitOfWork.UserRepository().GetById(id);
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _unitOfWork.UserRepository().GetByIdAsync(id);
        }

        public ApplicationUser GetByUserName(string userName)
        {
            return _unitOfWork.UserRepository().GetByUserName(userName);
        }

        public async Task<ApplicationUser> GetByUserNameAsync(string userName)
        {
            return await _unitOfWork.UserRepository().GetByUserNameAsync(userName);
        }

        public ApplicationUser GetByEmail(string email)
        {
            return _unitOfWork.UserRepository().GetByEmail(email);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _unitOfWork.UserRepository().GetByEmailAsync(email);
        }

        public bool Add(ApplicationUser user, string role)
        {
            _unitOfWork.UserRepository().Add(user, role);
            int result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> AddAsync(ApplicationUser user, string role)
        {
            _unitOfWork.UserRepository().Add(user, role);
            int result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Update(ApplicationUser user)
        {
            _unitOfWork.UserRepository().Update(user);
            int result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(ApplicationUser user)
        {
            _unitOfWork.UserRepository().Update(user);
            int result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Delete(string id)
        {
            _unitOfWork.UserRepository().Delete(id);
            int result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            _unitOfWork.UserRepository().Delete(id);
            int result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Delete(ApplicationUser user)
        {
            _unitOfWork.UserRepository().Delete(user);
            int result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(ApplicationUser user)
        {
            _unitOfWork.UserRepository().Delete(user);
            int result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public SignInStatus LogIn(LoginViewModel model)
        {
            ApplicationUser signedUser = _userManager.FindByEmail(model.Email);
            SignInStatus result =
                _signInManager.PasswordSignIn(signedUser.UserName, model.Password, model.RememberMe, false);
            return result;
        }

        public async Task<SignInStatus> LogInAsync(LoginViewModel model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            SignInStatus result =
                await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
            return result;
        }

        public void LogOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                string code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);

                var resetPasswordModel = new ResetPasswordViewModel
                {
                    Code = code,
                    Email = user.Email
                };

                await _emailService.SendResetPasswordMailAsync(resetPasswordModel);
                return ForgotPasswordConstants.Success;
            }

            return ForgotPasswordConstants.Error;
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded) return ResetPasswordConstants.Success;
            }

            return ResetPasswordConstants.Error;
        }

        public async Task<string> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                IdentityResult result =
                    await _userManager.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false, false);
                    return ChangePasswordConstants.Success;
                }
            }

            return ChangePasswordConstants.Error;
        }
    }

    #endregion

}