using System.ComponentModel.DataAnnotations;

namespace TS.Personal.Web.ViewModels
{
    public class UserProfileVm
    {
        public string UserId { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;
        
        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]        
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(25, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string? PhoneNumber { get; set; }
        
        public string? Gender { get; set; }
        
        public byte[]? ProfileImage { get; set; }
        
        public DateTime RegisteredDate { get; set; }
    }
}
