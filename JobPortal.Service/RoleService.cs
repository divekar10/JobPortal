using JobPortal.Database.Repo;
using JobPortal.Model;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Role> GetRoleById(int userId)
        {
            return await _roleRepository.GetById(userId);
        }
    }
}
