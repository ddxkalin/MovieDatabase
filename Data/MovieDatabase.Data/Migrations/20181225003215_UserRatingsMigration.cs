using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class UserRatingsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_RatedById",
                table: "UserRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "RatedById",
                table: "UserRating");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRating",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserRating",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserRating",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_UserId1",
                table: "UserRating",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId1",
                table: "UserRating",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId1",
                table: "UserRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_UserRating_UserId1",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserRating");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRating",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RatedById",
                table: "UserRating",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating",
                columns: new[] { "RatedById", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_RatedById",
                table: "UserRating",
                column: "RatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
