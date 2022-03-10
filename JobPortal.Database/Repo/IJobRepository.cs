using JobPortal.Database.Infra;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public interface IJobRepository : IRepository<Job>
    {
    }
}
