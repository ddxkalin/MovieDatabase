using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class makeRatingInheritBaseModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Rating",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Rating",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Rating",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Rating",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Rating",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_IsDeleted",
                table: "Rating",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rating_IsDeleted",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Rating");
        }
    }
}
