using System.ComponentModel.DataAnnotations;

namespace JavaFlorist.Models.DTO
{
    public class ChangePasswordModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
