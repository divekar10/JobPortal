using JobPortal.Database.Infra;
using JobPortal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public class ApplicantRepository : Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(JobDbContext jobDbContext) : base(jobDbContext)
        {
               
        }

        public async Task<IEnumerable<JobPostedApplicantDto>> GetAllApplicantAppliedToMyJobPosted(int userId)
        {
            var applicants = await (from a in JobDbContext.Applicant
                                    join u in JobDbContext.User on a.AppliedBy equals u.Id
                                    join j in JobDbContext.Job on a.JobId equals j.Id
                                    join us in JobDbContext.User on j.CreatedBy equals us.Id
                                    select new JobPostedApplicantDto
                                    {
                                        ApplicantName = u.Name,
                                        JobId = a.JobId,
                                        JobTitle = j.Title,
                                        RecruiterId = j.CreatedBy,
                                        RecruiterName = us.Name
                                    }).Where(x => x.RecruiterId == userId).OrderByDescending(x => x.JobId).ToListAsync();
            return applicants;
        }

        public async Task<IEnumerable<ApplicantDetailsDto>> GetAllApplicantJobApplied()
        {
            var getApplicants = await ( from a in JobDbContext.Applicant 
                                  join u in JobDbContext.User on a.AppliedBy equals u.Id
                                  join j in JobDbContext.Job on a.JobId equals j.Id
                                  select new ApplicantDetailsDto
                                  {
                                      UserId = u.Id,
                                      JobId = a.JobId,
                                      JobTitle = j.Title,
                                      ApplicantName = u.Name,
                                      PostedBy = j.CreatedBy
                                  }).OrderByDescending(x => x.JobId).ToListAsync();
            return getApplicants;
        }
    }
}
