using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IRoleService
    {
        Task<Role> GetRoleById(int userId);
    }
}
