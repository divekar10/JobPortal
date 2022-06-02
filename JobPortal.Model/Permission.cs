using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    [Table("Permission")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string SystemName { get; set; }
        public string Action { get; set; }
        public string ModuleName { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
