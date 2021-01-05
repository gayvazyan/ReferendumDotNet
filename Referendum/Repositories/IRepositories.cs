using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.Repositories
{
   
    public interface IRepositories<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity GetByID(int ID);
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        Task UpdateAsync(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        Task DeleteAsync(IEnumerable<TEntity> entities);
    }
}
