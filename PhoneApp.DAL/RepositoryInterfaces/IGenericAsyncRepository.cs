using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhoneApp.Models
{
    public interface IGenericAsyncRepository<T> where T : class
    {
        T Add(T t);
        Task<T> AddAsync(T t);
        int Count();
        Task<int> CountAsync();
        void Delete(T entity);
        Task<int> DeleteAsyn(T entity);
        void Dispose();
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        T FindById(int id);
        Task<T> FindByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsyn();
        IEnumerable<T> GetWithInclude(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        void Save();
        Task<int> SaveAsync();
        T Update(T t, object key);
        Task<T> UpdateAsyn(T t, object key);
    }
}
