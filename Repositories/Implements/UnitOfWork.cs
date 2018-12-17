using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Repositories.Interfaces;

namespace Repositories.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly WebContext _context;

        /// <summary>
        /// The repositories
        /// </summary>
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UnitOfWork(WebContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Gets generic repository
        /// </summary>
        /// <typeparam name="TEntity">The generic type</typeparam>
        /// <returns>The generic repository</returns>
        public IGenericRepository<TEntity> Repository<TEntity>()
            where TEntity : BaseEntity
        {
            if (this._repositories.Keys.Contains(typeof(TEntity)))
            {
                return this._repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }

            IGenericRepository<TEntity> repo = new GenericRepository<TEntity>(this._context);
            this._repositories.Add(typeof(TEntity), repo);
            return repo;
        }

        /// <summary>
        /// Commits the synchronous.
        /// </summary>
        /// <returns>
        /// The commit result
        /// </returns>
        public int Commit()
        {
            return this._context.SaveChanges();
        }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns>
        /// The commit result
        /// </returns>
        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    foreach (var repository in _repositories)
                    {
                        (repository.Value as IDisposable).Dispose();
                    }

                    _repositories.Clear();
                    this._context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
