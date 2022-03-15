using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IRecruiterService
    {
        Task<User> Add(User entity);
        IEnumerable<User> GetRecruiters();
    }
}
