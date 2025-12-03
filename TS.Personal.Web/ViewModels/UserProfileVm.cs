namespace TS.Personal.Web.ViewModels
{
    public class UserProfileVm
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public byte[]? ProfileImage { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
