using System;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public abstract class GenericService<TEntity>:IGenericService<TEntity> where TEntity:BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool _disposed;

        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public IQueryable<TEntity> GetAll()
        {
            return _unitOfWork.Repository<TEntity>().TableNoTracking;
        }
        public TEntity GetById(int id)
        {
            return _unitOfWork.Repository<TEntity>().GetById(id);
        }

        public bool Add(TEntity entity)
        {
            _unitOfWork.Repository<TEntity>().Add(entity);
            var result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            _unitOfWork.Repository<TEntity>().Add(entity);
            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Update(TEntity entity)
        {
            _unitOfWork.Repository<TEntity>().Update(entity);
            var result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _unitOfWork.Repository<TEntity>().Update(entity);
            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Delete(int id)
        {
            _unitOfWork.Repository<TEntity>().Delete(id);
            var result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _unitOfWork.Repository<TEntity>().Delete(id);
            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Delete(TEntity entity)
        {
            _unitOfWork.Repository<TEntity>().Delete(entity);
            var result = _unitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _unitOfWork.Repository<TEntity>().Delete(entity);
            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

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
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._unitOfWork.Dispose();
                }
            }

            this._disposed = true;
        }
    }
}
