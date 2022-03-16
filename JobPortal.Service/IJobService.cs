using JobPortal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IJobService
    {
        //Task<Job> GetJob();
        Task<Job> Add(Job entity, int userId);
        Task<IEnumerable<Job>> AddJobs(List<Job> entities);
        Task<IEnumerable<Job>> GetJobs(PagedParameters pagedParameters);
        Task<IEnumerable<Job>> GetMyJobs(int userId, PagedParameters pagedParameters);
        Task<Job> Update(Job entity);
        Task<bool> Delete(int id);
    }
}
