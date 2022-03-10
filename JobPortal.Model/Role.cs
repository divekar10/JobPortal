using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    public class Role
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify role")]
        [StringLength(25, ErrorMessage = "Maximum 25 characters")]
        public string Roles { get; set; }
    }
}
