using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    [Table("PermissionRoleMapping")]
    public class PermissionRoleMapping
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool IsActive { get; set; }
    }
}
