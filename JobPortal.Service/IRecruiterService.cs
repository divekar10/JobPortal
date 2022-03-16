using JobPortal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IRecruiterService
    {
        Task<User> Add(User entity);
        Task<IEnumerable<User>> GetRecruiters(PagedParameters pagedParameters);
    }
}
