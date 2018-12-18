using System.Linq;
using System.Threading.Tasks;
using Models;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        protected UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork => _unitOfWork;

        public IQueryable<ApplicationUser> GetAll()
        {
            return _unitOfWork.UserRepository().TableNoTracking;
        }
        public ApplicationUser GetById(string id)
        {
            return _unitOfWork.UserRepository().GetById(id);
        }

        public ApplicationUser GetByUserName(string userName)
        {
            return _unitOfWork.UserRepository().GetByUserName(userName);
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
