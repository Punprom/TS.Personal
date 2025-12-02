using System.ComponentModel.DataAnnotations;

namespace TS.Personal.Web.Models
{
    public class UserRegisteringModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Gender")]
        [StringLength(10, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string? Gender { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "LastName")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage="Birth date is required")]
        public DateTime BirthDate { get; set; } = DateTime.Now.Date;

        [StringLength(30, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        public byte[]? ProfileImage { get; set; }

                
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
