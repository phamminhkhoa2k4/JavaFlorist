using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JavaFlorist.Models.DTO
{
    public class RegistrationModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }


        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string? Password { get; set; }
        [Required]
        [DisplayName("Password Confirm")]
        [Compare("Password")]
        public string? PasswordConfirm { get; set; }
        public string? Role { get; set; }
    }
}
