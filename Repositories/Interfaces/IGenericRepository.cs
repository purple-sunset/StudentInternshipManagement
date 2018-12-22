﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Entities;

namespace Repositories.Interfaces
{
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
}