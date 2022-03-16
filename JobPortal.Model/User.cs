using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify name.")]
        [StringLength(75, ErrorMessage = "Maximum 75 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please specify email.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please specify password")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
