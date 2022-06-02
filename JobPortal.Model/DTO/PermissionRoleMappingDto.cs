using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model.DTO
{
    public class PermissionRoleMappingDto
    {
        public int? RoleId { get; set; }
        public int PermissionId { get; set; }
        public string EntityName { get; set; }
        public string SystemName { get; set; }
        public string Action { get; set; }
        public string ModuleName { get; set; }
        public string Type { get; set; }
        public bool IsAllowed { get; set; }
    }
}
