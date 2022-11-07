using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class MovieModelsToDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Keyword",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Keyword",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Comment",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CastMember",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CastMember",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Award",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Award",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_IsDeleted",
                table: "Keyword",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IsDeleted",
                table: "Comment",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CastMember_IsDeleted",
                table: "CastMember",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Award_IsDeleted",
                table: "Award",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Keyword_IsDeleted",
                table: "Keyword");

            migrationBuilder.DropIndex(
                name: "IX_Comment_IsDeleted",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_CastMember_IsDeleted",
                table: "CastMember");

            migrationBuilder.DropIndex(
                name: "IX_Award_IsDeleted",
                table: "Award");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CastMember");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CastMember");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Award");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Award");
        }
    }
}
