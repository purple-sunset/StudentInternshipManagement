using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Entities;
using Repositories.Interfaces;

namespace Services.Interfaces
{
    public interface IGenericService<TEntity> where TEntity:BaseEntity
    {
        //IUnitOfWork UnitOfWork { get; }
        IQueryable<TEntity> GetAll();
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        bool Add(TEntity entity);
        Task<bool> AddAsync(TEntity entity);
        bool Update(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        bool Delete(int id);
        Task<bool> DeleteAsync(int id);
        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
