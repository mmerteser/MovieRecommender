using Microsoft.EntityFrameworkCore;
using MovieRecommender.Core.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MovieRecommender.Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        DbSet<TEntity> Table { get; }


        IQueryable<TEntity> AsQueryable();
        Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter, bool noTracking = true
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsync(int id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);


        Task<bool> AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);
        bool Add(TEntity entity);
        bool AddRange(IEnumerable<TEntity> entities);
        bool Remove(TEntity entity);
        bool RemoveRange(IEnumerable<TEntity> entities);
        bool Update(TEntity entity);
        bool AddOrUpdate(TEntity entity);
        Task<int> DeleteByIdsAsync(IEnumerable<int> ids);
        Task<int> BulkDeleteAsync(Expression<Func<TEntity, bool>> filter);
        int BulkDelete(Expression<Func<TEntity, bool>> filter);
        Task<int> BulkDeleteAsync(IEnumerable<TEntity> entities);
        bool BulkUpdate(IEnumerable<TEntity> entities);
        Task<int> SaveAsync();
    }
}
