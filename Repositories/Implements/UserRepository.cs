using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using Models.Contexts;
using Models.Entities;
using Repositories.Interfaces;
using Utilities;

namespace Repositories.Implements
{
    public class UserRepository:IUserRepository
    {
        #region Fields

        private readonly WebContext _context;
        private IDbSet<ApplicationUser> _entities;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Ctor
        public UserRepository(WebContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        #endregion

        #region Utilities
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        #endregion

        #region Methods

        public virtual ApplicationUser GetById(string id)
        {
            return this.Entities.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        public virtual async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await this.Entities.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public virtual ApplicationUser GetByUserName(string userName)
        {
            return this.Entities.FirstOrDefault(x => !x.IsDeleted && x.UserName == userName);
        }

        public virtual async Task<ApplicationUser> GetByUserNameAsync(string userName)
        {
            return await this.Entities.FirstOrDefaultAsync(x => !x.IsDeleted && x.UserName == userName);
        }

        public virtual void Add(ApplicationUser user, string role)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                user.CreatedAt = DateTime.Now;
                user.IsDeleted = false;
                _userManager.Create(user, "Ab=123456789");

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
                    throw new ArgumentNullException("user");

                user.UpdatedAt = DateTime.Now;
                this._context.Entry(user).State = EntityState.Modified;

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
                var user = GetById(id);
                if (user == null)
                    throw new ArgumentNullException("user");

                user.UpdatedAt = DateTime.Now;
                user.IsDeleted = true;
                this._context.Entry(user).State = EntityState.Modified;

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
                    throw new ArgumentNullException("user");

                user.UpdatedAt = DateTime.Now;
                user.IsDeleted = true;
                this._context.Entry(user).State = EntityState.Modified;

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
            get
            {
                return this.Entities.Where(x => !x.IsDeleted);
            }
        }

        public virtual IQueryable<ApplicationUser> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking().Where(x => !x.IsDeleted);
            }
        }

        protected virtual IDbSet<ApplicationUser> Entities
        {
            get
            {
                if (this._entities == null)
                {
                    this._entities = this._context.Set<ApplicationUser>();
                }

                return this._entities;
            }
        }

        #endregion

    }
}
