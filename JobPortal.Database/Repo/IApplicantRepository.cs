using JobPortal.Database.Infra;
using JobPortal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
        Task<IEnumerable<ApplicantDetailsDto>> GetAllApplicantJobApplied(PagedParameters pagedParameters);
        Task<IEnumerable<JobPostedApplicantDto>> GetAllApplicantAppliedToMyJobPosted(int userId, PagedParameters pagedParameters);
    }
}
