using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Models.Contexts;
using Models.Entities;
using Repositories.Interfaces;
using Utilities;

namespace Repositories.Implements
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region Ctor

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public GenericRepository(WebContext context)
        {
            _context = context;
        }

        #endregion

        #region Utilities

        /// <summary>
        ///     Get full error
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <returns>Error</returns>
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
            foreach (var error in validationErrors.ValidationErrors)
                msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) +
                       Environment.NewLine;
            return msg;
        }

        #endregion

        #region Fields

        private readonly WebContext _context;
        private IDbSet<TEntity> _entities;

        #endregion

        #region Methods

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetById(int id)
        {
            return Entities.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        /// <summary>
        ///     Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.CreatedAt = DateTime.Now;
                entity.IsDeleted = false;
                Entities.Add(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        ///     Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Add(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.IsDeleted = false;
                    Entities.Add(entity);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        ///     Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.UpdatedAt = DateTime.Now;
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        ///     Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                {
                    entity.UpdatedAt = DateTime.Now;
                    _context.Entry(entity).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        ///     Delete entity
        /// </summary>
        /// <param name="id">Entity</param>
        public virtual void Delete(int id)
        {
            try
            {
                var entity = GetById(id);
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.UpdatedAt = DateTime.Now;
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }


        /// <summary>
        ///     Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.UpdatedAt = DateTime.Now;
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        ///     Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                {
                    entity.UpdatedAt = DateTime.Now;
                    entity.IsDeleted = true;
                    _context.Entry(entity).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //throw new Exception(this.GetFullErrorText(dbEx), dbEx);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table
        {
            get { return Entities.Where(x => !x.IsDeleted); }
        }

        /// <summary>
        ///     Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only
        ///     operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking
        {
            get { return Entities.AsNoTracking().Where(x => !x.IsDeleted); }
        }

        /// <summary>
        ///     Entities
        /// </summary>
        protected virtual IDbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null) _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

        #endregion
    }
}