using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Data.Repository.Contract
{
    public interface IRepositoryAsync<TEntity, in TKey> where TEntity : class
    {
        Task<TEntity> GetAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null);

        void AddAsync(TEntity entity);
    }
}