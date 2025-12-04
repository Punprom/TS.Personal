using TS.Personal.Core.Dtos;

namespace TS.Personal.Core.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserProfileAsync(string userId);

    Task<List<UserDto>> GetAllUsersAsync();

    Task UpdateUserProfileAsync(UserDto user);

    Task<byte[]?> GetUserProfileImageAsync(string userId);

    Task UpdateUserProfileImageAsync(string userId, byte[]? profileImage);
}
