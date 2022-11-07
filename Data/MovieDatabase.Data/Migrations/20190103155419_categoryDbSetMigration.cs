using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class categoryDbSetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_MovieCategories_MovieId",
                table: "MovieCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCategories",
                table: "MovieCategories");

            migrationBuilder.RenameTable(
                name: "MovieCategories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCategories_Slug",
                table: "Category",
                newName: "IX_Category_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCategories_IsDeleted",
                table: "Category",
                newName: "IX_Category_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Category_MovieId",
                table: "MovieCategory",
                column: "MovieId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Category_MovieId",
                table: "MovieCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "MovieCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Slug",
                table: "MovieCategories",
                newName: "IX_MovieCategories_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_Category_IsDeleted",
                table: "MovieCategories",
                newName: "IX_MovieCategories_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCategories",
                table: "MovieCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_MovieCategories_MovieId",
                table: "MovieCategory",
                column: "MovieId",
                principalTable: "MovieCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
