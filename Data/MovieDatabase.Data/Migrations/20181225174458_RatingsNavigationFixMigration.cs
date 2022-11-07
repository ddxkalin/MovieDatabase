using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class RatingsNavigationFixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieRating_AspNetUsers_RatingId",
                table: "MovieRating");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieRating_Rating_RatingId1",
                table: "MovieRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_RatingId",
                table: "UserRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_Rating_RatingId1",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_UserRating_RatingId1",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "RatingId1",
                table: "UserRating");

            migrationBuilder.RenameColumn(
                name: "RatingId1",
                table: "MovieRating",
                newName: "MovieId1");

            migrationBuilder.RenameIndex(
                name: "IX_MovieRating_RatingId1",
                table: "MovieRating",
                newName: "IX_MovieRating_MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRating_Movies_MovieId1",
                table: "MovieRating",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRating_Rating_RatingId",
                table: "MovieRating",
                column: "RatingId",
                principalTable: "Rating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_Rating_RatingId",
                table: "UserRating",
                column: "RatingId",
                principalTable: "Rating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId",
                table: "UserRating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieRating_Movies_MovieId1",
                table: "MovieRating");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieRating_Rating_RatingId",
                table: "MovieRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_Rating_RatingId",
                table: "UserRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_UserId",
                table: "UserRating");

            migrationBuilder.RenameColumn(
                name: "MovieId1",
                table: "MovieRating",
                newName: "RatingId1");

            migrationBuilder.RenameIndex(
                name: "IX_MovieRating_MovieId1",
                table: "MovieRating",
                newName: "IX_MovieRating_RatingId1");

            migrationBuilder.AddColumn<string>(
                name: "RatingId1",
                table: "UserRating",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_RatingId1",
                table: "UserRating",
                column: "RatingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRating_AspNetUsers_RatingId",
                table: "MovieRating",
                column: "RatingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRating_Rating_RatingId1",
                table: "MovieRating",
                column: "RatingId1",
                principalTable: "Rating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_RatingId",
                table: "UserRating",
                column: "RatingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_Rating_RatingId1",
                table: "UserRating",
                column: "RatingId1",
                principalTable: "Rating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
