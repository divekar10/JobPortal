using JobPortal.Database.Infra;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(JobDbContext jobDbContext) : base(jobDbContext)
        {

        }

        public IEnumerable<Job> GetMyJobs(int userId)
        {
            var myJobs = (from j in JobDbContext.Job
                          where j.CreatedBy == userId
                          select j).ToList();
            return myJobs;
        }
    }
}
