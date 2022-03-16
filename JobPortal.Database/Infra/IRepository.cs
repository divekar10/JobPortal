using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JobPortal.Database.Infra
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> AddAsync(List<T> entity);
        void Delete(T entity);
        void Update(T entity);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression);
        Task<T> GetDefault(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        IQueryable<T> FindAll();

    }
}
