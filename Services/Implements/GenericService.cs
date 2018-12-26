﻿using System.Linq;
using System.Threading.Tasks;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity
    {
        protected GenericService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public IQueryable<TEntity> GetAll()
        {
            return UnitOfWork.Repository<TEntity>().TableNoTracking;
        }

        public TEntity GetById(int id)
        {
            return UnitOfWork.Repository<TEntity>().GetById(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await UnitOfWork.Repository<TEntity>().GetByIdAsync(id);
        }

        public bool Add(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Add(entity);
            int result = UnitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Add(entity);
            int result = await UnitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Update(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Update(entity);
            int result = UnitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Update(entity);
            int result = await UnitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Delete(int id)
        {
            UnitOfWork.Repository<TEntity>().Delete(id);
            int result = UnitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            UnitOfWork.Repository<TEntity>().Delete(id);
            int result = await UnitOfWork.CommitAsync();
            return result > 0;
        }

        public bool Delete(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Delete(entity);
            int result = UnitOfWork.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Delete(entity);
            int result = await UnitOfWork.CommitAsync();
            return result > 0;
        }
    }
}