using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class RatingsNavigationManyToManyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieRating_Movies_MovieId1",
                table: "MovieRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId1",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_UserRating_UserId1",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_MovieRating_MovieId1",
                table: "MovieRating");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "MovieRating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserRating",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MovieId1",
                table: "MovieRating",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_UserId1",
                table: "UserRating",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRating_MovieId1",
                table: "MovieRating",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRating_Movies_MovieId1",
                table: "MovieRating",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId1",
                table: "UserRating",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
