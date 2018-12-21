using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;

        protected UserService(IUnitOfWork unitOfWork, ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
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

        public bool Add(ApplicationUser user, string role)
        {
            _unitOfWork.UserRepository().Add(user, role);
            var result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> AddAsync(ApplicationUser user, string role)
        {
            _unitOfWork.UserRepository().Add(user, role);
            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Update(ApplicationUser user)
        {
            _unitOfWork.UserRepository().Update(user);
            var result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(ApplicationUser user)
        {
            _unitOfWork.UserRepository().Update(user);
            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Delete(string id)
        {
            _unitOfWork.UserRepository().Delete(id);
            var result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            _unitOfWork.UserRepository().Delete(id);
            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Delete(ApplicationUser user)
        {
            _unitOfWork.UserRepository().Delete(user);
            var result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(ApplicationUser user)
        {
            _unitOfWork.UserRepository().Delete(user);
            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }
    }
}