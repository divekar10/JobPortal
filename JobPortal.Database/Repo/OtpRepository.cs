using JobPortal.Database.Infra;
using JobPortal.Model;

namespace JobPortal.Database.Repo
{
    public interface IOtpRepository : IRepository<UserOtp>
    {

    }
    public class OtpRepository : Repository<UserOtp>, IOtpRepository
    {
        public OtpRepository(JobDbContext jobDbContext) : base(jobDbContext)
        {

        }
    }
}
