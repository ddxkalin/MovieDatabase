using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class renamed_user_table_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailConfirmationTokenResentSentOn",
                table: "AspNetUsers",
                newName: "EmailConfirmationTokenResentOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailConfirmationTokenResentOn",
                table: "AspNetUsers",
                newName: "EmailConfirmationTokenResentSentOn");
        }
    }
}
