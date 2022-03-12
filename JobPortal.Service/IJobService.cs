using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IJobService
    {
        //Task<Job> GetJob();
        Task<Job> Add(Job entity, int userId);
        Task<IEnumerable<Job>> AddJobs(List<Job> entities);
        Task<IEnumerable<Job>> GetJobs();
        IEnumerable<Job> GetMyJobs(int userId);
        Task<Job> Update(Job entity);
        Task<bool> Delete(int id);
    }
}
