namespace TS.Personal.Core.Dtos;
public class UserDto
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Gender { get; set; }
    /// <summary>
    /// public byte[]? ProfileImage { get; set; }
    /// </summary>
    public DateTime RegisteredDate { get; set; }

}
