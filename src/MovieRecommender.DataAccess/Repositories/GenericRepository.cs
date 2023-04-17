using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieRecommender.Application.Repositories;
using MovieRecommender.Core.Entities;
using MovieRecommender.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.DataAccess.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MovieDbContext _context;

        public GenericRepository(MovieDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> AsQueryable() => Table.AsQueryable();

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(bool noTracking = true)
        {
            var query = Table.AsQueryable();
            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            T found = await Table.FindAsync(id);

            if (found is null)
                return null;

            if (noTracking)
                _context.Entry(found).State = EntityState.Detached;

            foreach (var include in includes)
            {
                _context.Entry(found).Reference(include).Load();
            }

            return found;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter, bool noTracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table;

            if (filter != null)
                query = query.Where(filter);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
                query = orderBy(query);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table;

            if (filter != null)
                query = query.Where(filter);

            if (noTracking)
                query = query.AsNoTracking();

            query = ApplyIncludes(query, includes);

            return await query.SingleOrDefaultAsync();
        }

        private IQueryable<T> ApplyIncludes(IQueryable<T> query, Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var includeItem in includes)
                {
                    query = query.Include(includeItem);
                }
            }

            return query;
        }

        public bool Add(T entity)
        {
            var entityEntry = Table.Add(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddAsync(T entity)
        {
            var entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public bool AddOrUpdate(T entity)
        {
            if (!Table.Local.Any(i => EqualityComparer<int>.Default.Equals(i.Id, entity.Id)))
                _context.Update(entity);
            else
                _context.Add(entity);

            return true;
        }

        public bool AddRange(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                return false;

            Table.AddRange(entities);
            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                await Task.CompletedTask;

            await Table.AddRangeAsync(entities);
            return true;
        }


        public async Task<int> BulkDeleteAsync(Expression<Func<T, bool>> filter)
        {
            var result = await Table.Where(filter).ExecuteDeleteAsync();
            return result;
        }

        public int BulkDelete(Expression<Func<T, bool>> filter)
        {
            var result = Table.Where(filter).ExecuteDelete();
            return result;
        }

        public async Task<int> BulkDeleteAsync(IEnumerable<T> entities)
        {
            var result = await Table.Where(i => entities.Equals(i)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<int> DeleteByIdsAsync(IEnumerable<int> ids)
        {
            var result = await Table.Where(i => ids.Equals(i.Id)).ExecuteDeleteAsync();
            return result;
        }

        public bool BulkUpdate(IEnumerable<T> entities)
        {
            foreach (var entityItem in entities)
            {
                Table.Attach(entityItem);
                _context.Entry(entityItem).State = EntityState.Modified;
            }
            return true;
        }


        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public bool Update(T entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
