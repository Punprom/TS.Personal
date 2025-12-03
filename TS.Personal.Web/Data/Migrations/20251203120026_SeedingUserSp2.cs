using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TS.Personal.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedingUserSp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                CREATE PROCEDURE [dbo].[GetUserProfiling]
                  @userId nvarchar(256)
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
                WHERE Id = @userId

                END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetUserProfiling]");
        }
    }
}
