using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class UserRatingsForeignKeyFixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_UserRating_UserId",
                table: "UserRating");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRating",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_Id",
                table: "UserRating",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_Id",
                table: "UserRating");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRating",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_UserId",
                table: "UserRating",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId",
                table: "UserRating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
