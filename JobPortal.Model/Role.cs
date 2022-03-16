using System.ComponentModel.DataAnnotations;

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
