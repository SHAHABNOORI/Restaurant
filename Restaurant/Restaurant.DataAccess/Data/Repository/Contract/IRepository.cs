using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Restaurant.DataAccess.Data.Repository.Contract
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Get(TKey id);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null);

        void Add(TEntity entity);

        void Remove(TKey id);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entity);
    }
}