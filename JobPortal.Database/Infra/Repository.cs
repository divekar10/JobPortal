using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database.Infra
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly JobDbContext _jobDbContext;
        public Repository(JobDbContext jobDbContext)
        {
            _jobDbContext = jobDbContext;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _jobDbContext.AddAsync<T>(entity);
            await _jobDbContext.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<List<T>> AddAsync(List<T> entity)
        {
            await _jobDbContext.AddRangeAsync(entity);
            await _jobDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await _jobDbContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await _jobDbContext.Set<T>().ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _jobDbContext.Set<T>().Remove(entity);
            _jobDbContext.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            _jobDbContext.Entry(entity).State = EntityState.Modified;
            _jobDbContext.Set<T>().Update(entity);
            _jobDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression)
        {
            return await _jobDbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _jobDbContext.Set<T>().FindAsync(id);
        }
    }
}
