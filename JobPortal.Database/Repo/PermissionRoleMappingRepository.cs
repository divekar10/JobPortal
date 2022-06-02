using JobPortal.Database.Infra;
using JobPortal.Model;
using JobPortal.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public interface IPermissionRoleMappingRepository : IRepository<PermissionRoleMapping>
    {
        IEnumerable<PermissionRoleMappingDto> GetPermissionsByRoleId(int id);
    }
    public class PermissionRoleMappingRepository : Repository<PermissionRoleMapping>, IPermissionRoleMappingRepository
    {
        public PermissionRoleMappingRepository(JobDbContext jobDbContext) : base(jobDbContext)
        {

        }

        public IEnumerable<PermissionRoleMappingDto> GetPermissionsByRoleId(int roleId)
        {
            var permissions = (from p in JobDbContext.Permission
                               join prm in JobDbContext.PermissionRoleMapping on p.Id equals prm.PermissionId
                               join r in JobDbContext.Role on prm.RoleId equals r.Id
                               where r.Id == roleId
                               select new PermissionRoleMappingDto
                               {
                                   RoleId = prm.RoleId,
                                   PermissionId = p.Id,
                                   EntityName = p.EntityName,
                                   SystemName = p.SystemName,
                                   Action = p.Action,
                                   ModuleName = p.ModuleName,
                                   Type = p.Type,
                                   IsAllowed = (prm.Id != 0),
                               }).ToList();
            return permissions;
        }
    }
}
