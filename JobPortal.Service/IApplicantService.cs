using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IApplicantService
    {
        Task<Applicant> Apply(Applicant entity, int userId);
        Task<IEnumerable<Applicant>> Apply(List<Applicant> entities);
        Task<IEnumerable<ApplicantDetailsDto>> GetAllApplicantJobApplied();
    }
}
