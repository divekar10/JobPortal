using JobPortal.Model;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IRoleService
    {
        Task<Role> GetRoleById(int userId);
    }
}
