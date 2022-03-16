using JobPortal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IApplicantService
    {
        Task<Applicant> Apply(Applicant entity, int userId);
        Task<IEnumerable<Applicant>> Apply(List<Applicant> entities);
        Task<IEnumerable<ApplicantDetailsDto>> GetAllApplicantJobApplied(PagedParameters pagedParameters);
        Task<IEnumerable<JobPostedApplicantDto>> GetAllApplicantAppliedToMyJobPosted(int userId, PagedParameters pagedParameters);
    }
}
