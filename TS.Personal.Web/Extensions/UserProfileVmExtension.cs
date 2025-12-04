using TS.Personal.Core.Dtos;
using TS.Personal.Web.ViewModels;

namespace TS.Personal.Web.Extensions;

public static class UserProfileVmExtension
{
    public static UserDto ToDto(this UserProfileVm source)
    {
        return new UserDto
        {
            Id = source.UserId,
            Email = source.Email,
            FirstName = source.FirstName,
            LastName = source.LastName,
            DateOfBirth = source.DateOfBirth,
            Gender = source.Gender,
            PhoneNumber = source.PhoneNumber
        };
    }
}