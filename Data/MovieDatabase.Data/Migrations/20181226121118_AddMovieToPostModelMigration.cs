using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class AddMovieToPostModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "MovieId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_MovieId",
                table: "Posts",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Movies_MovieId",
                table: "Posts",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Movies_MovieId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_MovieId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Posts");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Posts",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
