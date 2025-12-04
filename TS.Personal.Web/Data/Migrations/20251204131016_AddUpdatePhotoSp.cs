using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TS.Personal.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatePhotoSp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                CREATE PROCEDURE [dbo].[UpdateUserProfileImage]
                    @userId nvarchar(256),
                    @img varbinary(MAX)
                AS
                BEGIN
	
	                UPDATE [dbo].[AspNetUsers]
	                 SET ProfileImage = @img	 
	                WHERE Id = @userId
                END
            ";
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[UpdateUserProfileImage]");
        }
    }
}
