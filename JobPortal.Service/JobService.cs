using JobPortal.Database.Repo;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;   
        }
        public Task<Job> Add(Job entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Job>> AddJobs(List<Job> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Job>> GetJobs()
        {
            throw new NotImplementedException();
        }

        public Task<Job> Update(Job entity)
        {
            throw new NotImplementedException();
        }
    }
}
