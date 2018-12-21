using System.Threading.Tasks;
using Models.Entities;

namespace Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Gets generic repository
        /// </summary>
        /// <typeparam name="TEntity">The generic type</typeparam>
        /// <returns>The generic repository</returns>
        IGenericRepository<TEntity> Repository<TEntity>()
            where TEntity : BaseEntity;

        /// <summary>
        ///     Get user repository
        /// </summary>
        /// <returns></returns>
        IUserRepository UserRepository();

        /// <summary>
        ///     Commits the synchronous.
        /// </summary>
        /// <returns>The commit result</returns>
        int Commit();

        /// <summary>
        ///     Commits the asynchronous.
        /// </summary>
        /// <returns>The commit result</returns>
        Task<int> CommitAsync();
    }
}