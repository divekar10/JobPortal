using JobPortal.Database.Infra;
using JobPortal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<IEnumerable<Job>> GetMyJobs(int userId, PagedParameters pagedParameters);
        Task<IEnumerable<Job>> Jobs(PagedParameters pagedParameters);
    }
}
