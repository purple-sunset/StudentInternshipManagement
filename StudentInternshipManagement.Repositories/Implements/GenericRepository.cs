using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Utilities;

namespace StudentInternshipManagement.Repositories.Implements
{
    #region Interface

    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        ///     Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        ///     Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only
        ///     operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity GetById(int id);

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        ///     Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Add(TEntity entity);

        /// <summary>
        ///     Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        ///     Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Delete entity
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        ///     Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        ///     Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<TEntity> entities);
    }

    #endregion

    #region Class

    public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region Ctor

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public GenericRepository(DbContext context)
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
        // ReSharper disable once UnusedMember.Local
        private string GetFullErrorText(DbEntityValidationException exc)
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
        private IDbSet<TEntity> _entities;

        #endregion

        #region Methods

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public TEntity GetById(int id)
        {
            return Entities.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        /// <summary>
        ///     Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

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
        public void Add(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (TEntity entity in entities)
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
        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

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
        public void Update(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (TEntity entity in entities)
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
        public void Delete(int id)
        {
            try
            {
                TEntity entity = GetById(id);
                if (entity == null)
                    throw new ArgumentNullException(nameof(id));

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
        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

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
        public void Delete(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (TEntity entity in entities)
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
        public IQueryable<TEntity> Table
        {
            get { return Entities.Where(x => !x.IsDeleted); }
        }

        /// <summary>
        ///     Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only
        ///     operations
        /// </summary>
        public IQueryable<TEntity> TableNoTracking
        {
            get { return Entities.AsNoTracking().Where(x => !x.IsDeleted); }
        }

        /// <summary>
        ///     Entities
        /// </summary>
        private IDbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());

        #endregion
    }

    #endregion

}