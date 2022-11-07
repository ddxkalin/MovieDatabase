using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class movieCategoryUniqueSlugMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "MovieCategories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieCategories_Slug",
                table: "MovieCategories",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieCategories_Slug",
                table: "MovieCategories");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "MovieCategories");
        }
    }
}
