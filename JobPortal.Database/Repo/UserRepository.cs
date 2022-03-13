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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(JobDbContext jobDbContext) : base(jobDbContext)
        {

        }

        public IEnumerable<User> GetCandidates()
        {
            var candidates = (from u in JobDbContext.User
                              where u.RoleId == 1
                              select u).ToList();
            return candidates;
        }

        public async Task<IEnumerable<AppliedJobDto>> GetMyAllJobsApplied(int userId)
        {
            var myJobs = await (from a in JobDbContext.Applicant
                          join j in JobDbContext.Job on a.JobId equals j.Id
                          where a.AppliedBy == userId
                          select new AppliedJobDto
                          {
                              UserId = a.AppliedBy,
                              JobId = a.JobId,
                              JobTitle = j.Title
                          }).ToListAsync();
            return myJobs;
        }

        public IEnumerable<User> GetRecruiters()
        {
            var recruiters = (from u in JobDbContext.User
                              where u.RoleId == 2
                              select u).ToList();
            return recruiters;
        }
    }
}
