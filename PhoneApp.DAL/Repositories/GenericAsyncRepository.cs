using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhoneApp.Models
{
    public abstract class GenericAsyncRepository<T> : IGenericAsyncRepository<T> where T : class
    {
        protected DbContext _context;
        DbSet<T> _dbSet;

        public GenericAsyncRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }


        public IQueryable<T> Get()
        {
            return _dbSet.AsNoTracking();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public virtual T FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual T Add(T t)
        {
            _dbSet.Add(t);
            _context.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsync(T t)
        {
            _dbSet.Add(t);
            await _context.SaveChangesAsync();
            return t;

        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _dbSet.SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbSet.SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _dbSet.Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _dbSet.Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _dbSet.Find(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                _context.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsyn(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _dbSet.FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public virtual void Save()
        {

            _context.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }


        public IQueryable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }

        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
