using JobPortal.Database.Infra;
using JobPortal.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(JobDbContext jobDbContext) : base(jobDbContext)
        {

        }

        public async Task<IEnumerable<Job>> Jobs(PagedParameters pagedParameters)
        {
            var jobs = await (from j in JobDbContext.Job
                        select j)
                        .Skip((pagedParameters.PageNumber - 1) * pagedParameters.PageSize)
                        .Take(pagedParameters.PageSize)
                        .ToListAsync();
            return jobs;
        }
        public async Task<IEnumerable<Job>> GetMyJobs(int userId, PagedParameters pagedParameters)
        {
            var myJobs = await (from j in JobDbContext.Job
                          where j.CreatedBy == userId
                          select j)
                          .Skip((pagedParameters.PageNumber -1 ) * pagedParameters.PageSize)
                          .Take(pagedParameters.PageSize)
                          .ToListAsync();
            return myJobs;
        }
    }
}
