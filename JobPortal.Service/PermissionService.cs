
using JobPortal.Service.Caching;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace JobPortal.Service
{
    public class PermissionService : IPermissionService
    {
        private const string PERMISSION_ALLOWED_KEY = "permission.allowed-{0}-{1}";

        private readonly IHttpContextAccessor _context;
        private readonly ICacheManager _cacheManager;
        private readonly IPermissionRoleMappingService _permissionRoleMappingService;
        public PermissionService(IHttpContextAccessor httpContextAccessor,
                                 ICacheManager cacheManager,
                                 IPermissionRoleMappingService permissionRoleMappingService)
        {
            _context = httpContextAccessor;
            _cacheManager = cacheManager;
            _permissionRoleMappingService = permissionRoleMappingService;
        }

        public bool Authorize(string permissionSystemName)
        {
            if (string.IsNullOrEmpty(permissionSystemName))
                return false;

            var roleId = Convert.ToInt32(_context.HttpContext.User.Claims.First(i => i.Type == "RoleId").Value);
            var key = string.Format(PERMISSION_ALLOWED_KEY,roleId, permissionSystemName);

            return _cacheManager.Get(key, () =>
            {
                var permissions = _permissionRoleMappingService.GetPermissionsByRoleId(roleId).Where(x => x.IsAllowed == true);

                foreach (var permission in permissions)
                    if (permission.SystemName.Equals(permissionSystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                    return false;
            });
        }
    }
}
