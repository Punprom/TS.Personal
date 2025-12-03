using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TS.Personal.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedingUserSps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                CREATE PROCEDURE [dbo].[ListAllUsers]
                AS
                BEGIN
	
	                SELECT Id, FirstName, LastName, Email,Gender, 
		                DateOfBirth,
		                PhoneNumber,
		                EmailConfirmed,  
		                CASE WHEN PhoneNumber IS NULL THEN 0
			                 ELSE  1
		                END [PhoneNumberConfirmed], ProfileImage, RegisteredDate
	                FROM [dbo].[AspNetUsers]

                END
                GO

                CREATE PROCEDURE [dbo].[UpdateUserProfile]
                @userId varchar(256),
                @firstName nvarchar(30), 
                @lastName nvarchar(30),
                @gender nvarchar(10), 
                @dateOfBirth datetime2,
                @phoneNumber nvarchar(30),
                @profileImage varbinary(MAX)
                AS
                BEGIN

	                UPDATE [dbo].[AspNetUsers]
	                 SET FirstName = @firstName,
		                LastName = @lastName,
		                Gender = @gender,
		                DateOfBirth = @dateOfBirth,
		                PhoneNumber = @phoneNumber,
		                PhoneNumberConfirmed = CASE 
								                WHEN [PhoneNumber] IS NOT NULL THEN 1
								                ELSE 0
							                   END,
		                ProfileImage = @profileImage
	                WHERE Id = @userId
                END
               GO
            ";
            migrationBuilder.Sql(sp);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[ListAllUsers]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[UpdateUserProfile]");
        }
    }
}
