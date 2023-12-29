using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using JavaFlorist.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.DTO
{
    public class UserCustomerModel
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

        public string? F_name { get; set; }

        public string? L_name { get; set; }

        public DateTime Dob { get; set; }



        public string? Gender { get; set; }

        public int P_no { get; set; }

        public string? Address { get; set; }

        public string?   ProfilePicture { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
