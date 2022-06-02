using JobPortal.Database.Repo;
using JobPortal.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IPermissionRoleMappingService
    {
        IEnumerable<PermissionRoleMappingDto> GetPermissionsByRoleId(int id);
    }
    public class PermissionRoleMappingService : IPermissionRoleMappingService
    {
        private readonly IPermissionRoleMappingRepository _permissionRoleMappingRepository;
        public PermissionRoleMappingService(IPermissionRoleMappingRepository permissionRoleMappingRepository)
        {
            _permissionRoleMappingRepository = permissionRoleMappingRepository;
        }

        public IEnumerable<PermissionRoleMappingDto> GetPermissionsByRoleId(int id)
        {
            return _permissionRoleMappingRepository.GetPermissionsByRoleId(id);
        }
    }
}
