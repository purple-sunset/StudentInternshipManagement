using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// Gets generic repository
        /// </summary>
        /// <typeparam name="TEntity">The generic type</typeparam>
        /// <returns>The generic repository</returns>
        IGenericRepository<TEntity> Repository<TEntity>()
            where TEntity : BaseEntity;

        /// <summary>
        /// Commits the synchronous.
        /// </summary>
        /// <returns>The commit result</returns>
        int Commit();

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns>The commit result</returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        new void Dispose();
    }
}
