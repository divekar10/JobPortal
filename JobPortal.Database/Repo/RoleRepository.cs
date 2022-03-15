using JobPortal.Database.Infra;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(JobDbContext jobDbContext) :base(jobDbContext)
        {

        }
    }
}
