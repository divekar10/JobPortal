using JobPortal.Database.Infra;
using JobPortal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetRecruiters(PagedParameters pagedParameters);
        Task<IEnumerable<User>> GetCandidates(PagedParameters pagedParameters);
        Task<IEnumerable<User>> GetUsers(PagedParameters pagedParameters);
        Task<IEnumerable<AppliedJobDto>> GetMyAllJobsApplied(int userId, PagedParameters pagedParameters);
        //string GetEmailbody(string type);
    }
}
