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
        protected JobDbContext JobDbContext { get; set; }
        public string connectionString = string.Empty;
        //private readonly JobDbContext _jobDbContext;
        public Repository(JobDbContext jobDbContext)
        {
            this.JobDbContext = jobDbContext;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await JobDbContext.AddAsync<T>(entity);
            await JobDbContext.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<List<T>> AddAsync(List<T> entity)
        {
            await JobDbContext.AddRangeAsync(entity);
            await JobDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await JobDbContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await JobDbContext.Set<T>().ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            JobDbContext.Set<T>().Remove(entity);
            JobDbContext.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            JobDbContext.Entry(entity).State = EntityState.Modified;
            JobDbContext.Set<T>().Update(entity);
            JobDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression)
        {
            return await JobDbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await JobDbContext.Set<T>().FindAsync(id);
        }
    }
}
