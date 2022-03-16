using System.ComponentModel.DataAnnotations;

namespace JobPortal.Model
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password.")]
        public string Password { get; set; }
    }
}
