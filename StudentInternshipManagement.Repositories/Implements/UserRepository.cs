using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using StudentInternshipManagement.Models.Constants;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Utilities;

namespace StudentInternshipManagement.Repositories.Implements
{
    #region Interface

    public interface IUserRepository
    {
        IQueryable<ApplicationUser> Table { get; }

        IQueryable<ApplicationUser> TableNoTracking { get; }
        ApplicationUser GetById(string id);
        Task<ApplicationUser> GetByIdAsync(string id);

        ApplicationUser GetByUserName(string userName);
        Task<ApplicationUser> GetByUserNameAsync(string userName);

        ApplicationUser GetByEmail(string email);
        Task<ApplicationUser> GetByEmailAsync(string email);

        void Add(ApplicationUser user, string role);

        void Update(ApplicationUser user);

        void Delete(string id);

        void Delete(ApplicationUser user);
    }

    #endregion

    #region Class

    public class UserRepository : IUserRepository
    {
        #region Ctor

        public UserRepository(DbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region Utilities

        // ReSharper disable once UnusedMember.Global
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            string msg = string.Empty;
            foreach (DbEntityValidationResult validationErrors in exc.EntityValidationErrors)
                foreach (DbValidationError error in validationErrors.ValidationErrors)
                    msg += $"Property: {error.PropertyName} Error: {error.ErrorMessage}" +
                           Environment.NewLine;
            return msg;
        }

        #endregion

        #region Fields

        private readonly DbContext _context;
        private IDbSet<ApplicationUser> _entities;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Methods

        public virtual ApplicationUser GetById(string id)
        {
            return Entities.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        public virtual async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await Entities.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public virtual ApplicationUser GetByUserName(string userName)
        {
            return Entities.FirstOrDefault(x => !x.IsDeleted && x.UserName == userName);
        }

        public virtual async Task<ApplicationUser> GetByUserNameAsync(string userName)
        {
            return await Entities.FirstOrDefaultAsync(x => !x.IsDeleted && x.UserName == userName);
        }

        public ApplicationUser GetByEmail(string email)
        {
            return Entities.FirstOrDefault(x => !x.IsDeleted && x.Email == email);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await Entities.FirstOrDefaultAsync(x => !x.IsDeleted && x.Email == email);
        }

        public virtual void Add(ApplicationUser user, string role)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                user.CreatedAt = DateTime.Now;
                user.IsDeleted = false;
                _userManager.Create(user, AccountConstants.DefaultPassword);

                _userManager.AddToRole(user.Id, role);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Update(ApplicationUser user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                user.UpdatedAt = DateTime.Now;
                _context.Entry(user).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Delete(string id)
        {
            try
            {
                ApplicationUser user = GetById(id);
                if (user == null)
                    throw new ArgumentNullException(nameof(id));

                user.UpdatedAt = DateTime.Now;
                user.IsDeleted = true;
                _context.Entry(user).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Delete(ApplicationUser user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                user.UpdatedAt = DateTime.Now;
                user.IsDeleted = true;
                _context.Entry(user).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        #endregion

        #region Properties

        public virtual IQueryable<ApplicationUser> Table
        {
            get { return Entities.Where(x => !x.IsDeleted); }
        }

        public virtual IQueryable<ApplicationUser> TableNoTracking
        {
            get { return Entities.AsNoTracking().Where(x => !x.IsDeleted); }
        }

        protected virtual IDbSet<ApplicationUser> Entities =>
            _entities ?? (_entities = _context.Set<ApplicationUser>());

        #endregion
    }

    #endregion

}