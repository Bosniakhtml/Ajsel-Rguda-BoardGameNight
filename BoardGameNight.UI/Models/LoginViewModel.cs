using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BoardGameNight.UI.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [UIHint("Password")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
        public string? ReturnUrl { get; set; } = "/";
    }
}
