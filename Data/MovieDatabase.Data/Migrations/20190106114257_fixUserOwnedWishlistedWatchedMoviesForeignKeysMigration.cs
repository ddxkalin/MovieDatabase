using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class fixUserOwnedWishlistedWatchedMoviesForeignKeysMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOwnedMovie_AspNetUsers_MovieId",
                table: "UserOwnedMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOwnedMovie_Movies_UserId",
                table: "UserOwnedMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWatchedMovie_AspNetUsers_MovieId",
                table: "UserWatchedMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWatchedMovie_Movies_UserId",
                table: "UserWatchedMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWishlist_AspNetUsers_MovieId",
                table: "UserWishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWishlist_Movies_UserId",
                table: "UserWishlist");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOwnedMovie_Movies_MovieId",
                table: "UserOwnedMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOwnedMovie_AspNetUsers_UserId",
                table: "UserOwnedMovie",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchedMovie_Movies_MovieId",
                table: "UserWatchedMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchedMovie_AspNetUsers_UserId",
                table: "UserWatchedMovie",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWishlist_Movies_MovieId",
                table: "UserWishlist",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWishlist_AspNetUsers_UserId",
                table: "UserWishlist",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOwnedMovie_Movies_MovieId",
                table: "UserOwnedMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOwnedMovie_AspNetUsers_UserId",
                table: "UserOwnedMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWatchedMovie_Movies_MovieId",
                table: "UserWatchedMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWatchedMovie_AspNetUsers_UserId",
                table: "UserWatchedMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWishlist_Movies_MovieId",
                table: "UserWishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWishlist_AspNetUsers_UserId",
                table: "UserWishlist");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOwnedMovie_AspNetUsers_MovieId",
                table: "UserOwnedMovie",
                column: "MovieId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOwnedMovie_Movies_UserId",
                table: "UserOwnedMovie",
                column: "UserId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchedMovie_AspNetUsers_MovieId",
                table: "UserWatchedMovie",
                column: "MovieId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchedMovie_Movies_UserId",
                table: "UserWatchedMovie",
                column: "UserId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWishlist_AspNetUsers_MovieId",
                table: "UserWishlist",
                column: "MovieId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWishlist_Movies_UserId",
                table: "UserWishlist",
                column: "UserId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
