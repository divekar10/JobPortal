using JobPortal.Database.Infra;
using JobPortal.Model;

namespace JobPortal.Database.Repo
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(JobDbContext jobDbContext) :base(jobDbContext)
        {

        }
    }
}
