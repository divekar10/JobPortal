using JobPortal.Database.Infra;
using JobPortal.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(JobDbContext jobDbContext) : base(jobDbContext)
        {

        }

        public async Task<IEnumerable<User>> GetCandidates(PagedParameters pagedParameters)
        {
            var candidates = await (from u in JobDbContext.User
                              where u.RoleId == 1
                              select u)
                              .Skip((pagedParameters.PageNumber - 1) * pagedParameters.PageSize)
                              .Take(pagedParameters.PageSize)
                              .ToListAsync();
            return candidates;
        }

        public async Task<IEnumerable<AppliedJobDto>> GetMyAllJobsApplied(int userId, PagedParameters pagedParameters)
        {
            var myJobs = await (from a in JobDbContext.Applicant
                          join j in JobDbContext.Job on a.JobId equals j.Id
                          where a.AppliedBy == userId
                          select new AppliedJobDto
                          {
                              UserId = a.AppliedBy,
                              JobId = a.JobId,
                              JobTitle = j.Title
                          })
                          .Skip((pagedParameters.PageNumber - 1) * pagedParameters.PageSize)
                          .Take(pagedParameters.PageSize)
                          .ToListAsync();
            return myJobs;
        }

        public async Task<IEnumerable<User>> GetRecruiters(PagedParameters pagedParameters)
        {
            var recruiters = await (from u in JobDbContext.User
                              where u.RoleId == 2
                              select u)
                              .Skip((pagedParameters.PageNumber - 1) * pagedParameters.PageSize)
                              .Take(pagedParameters.PageSize)
                              .ToListAsync();
            return recruiters;
        }

        public async Task<IEnumerable<User>> GetUsers(PagedParameters pagedParameters)
        {
            var users = await (from u in JobDbContext.User
                         select u)
                         .Skip((pagedParameters.PageNumber - 1) * pagedParameters.PageSize)
                         .Take(pagedParameters.PageSize)
                         .ToListAsync();
            return users;
        }

        //public string GetEmailbody(string type)
        //{
        //    var emailBody = (from j in JobDbContext.EmailTemplate
        //                     where j.Type == type
        //                     select j).ToString();
        //    return emailBody;
        //}
    }
}
