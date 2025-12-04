using Dapper;
using System.Data;
using System.Data.Common;
using TS.Personal.Core.Dtos;
using TS.Personal.Core.Interfaces;

namespace TS.Personal.Security.Services
{
    internal class UserService : IUserService
    {
        private readonly Func<IDbConnection> _connectionFactory;

        public UserService(Func<IDbConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var sqlCmd = "EXEC [dbo].[ListAllUsers]";
            using var connection = _connectionFactory();
            connection.Open(); // Use synchronous Open() since IDbConnection does not have OpenAsync

            var users = await connection.QueryAsync<UserDto>(sqlCmd);
            
            return users.ToList();
        }

        public async Task<UserDto?> GetUserProfileAsync(string userId)
        {
            UserDto? result = null!;

            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);

            var sqlCmd = "EXEC [dbo].[GetUserProfiling] @userId";
            using var connection = _connectionFactory();
            connection.Open(); 
            result = await connection.QueryFirstOrDefaultAsync<UserDto>(sqlCmd, parameters);

            return result;
        }

        public async Task<byte[]?> GetUserProfileImageAsync(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@uid", userId);

            var sqlCmd = @"SELECT ProfileImage 
                           FROM [dbo].[AspNetUsers] 
                           WHERE Id = @uid";
            
            using var connection = _connectionFactory();
            switch (connection)
            {
                case DbConnection dbConnection:
                    return await dbConnection.QueryFirstOrDefaultAsync<byte[]>(sqlCmd, parameters);
                default:
                    connection.Open();
                    return connection.QueryFirstOrDefault<byte[]>(sqlCmd, parameters);
            }
        }

        public async Task UpdateUserProfileAsync(UserDto user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@userId", user.Id);
            parameters.Add("@firstName", user.FirstName);
            parameters.Add("@lastName", user.LastName);
            parameters.Add("@gender", user.Gender);
            parameters.Add("@dateOfBirth", user.DateOfBirth);
            parameters.Add("@phoneNumber", user.PhoneNumber);
            
            var sqlCmd = @"EXEC [dbo].[UpdateUserProfile] @userId, @firstName, @lastName, 
                            @gender, @dateOfBirth, @phoneNumber";
            using var connection = _connectionFactory();
            connection.Open(); 

            await connection.ExecuteAsync(sqlCmd, parameters);
        }

        public async Task UpdateUserProfileImageAsync(string userId, byte[]? profileImage)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);
            parameters.Add("@img", profileImage);

            var sqlCmd = @"UPDATE [dbo].[AspNetUsers]
	                         SET ProfileImage = @img	 
	                       WHERE Id = @userId";

            using var connection = _connectionFactory();

            if (connection is DbConnection dbConnection)
            {
                await dbConnection.OpenAsync();
            }
            else
            {
                connection.Open();
            }

            // Await the ExecuteAsync while the connection is still open and in scope.
            await connection.ExecuteAsync(new CommandDefinition(sqlCmd, parameters));
        }
    }
}
