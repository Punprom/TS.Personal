using Microsoft.AspNetCore.Identity;

namespace TS.Personal.Web.Data;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; } 

    public string? Gender { get; set; }

    public byte[]? ProfileImage { get; set; }

    public DateTime RegisteredDate { get; set; }

}
