using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class RatingsNavigationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieRating_AspNetUsers_MovieId",
                table: "MovieRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_Id",
                table: "UserRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "RatedOn",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "RatedOn",
                table: "MovieRating");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "MovieRating");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserRating",
                newName: "RatingId");

            migrationBuilder.RenameColumn(
                name: "RatedById",
                table: "MovieRating",
                newName: "RatingId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRating",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RatingId1",
                table: "UserRating",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RatingId1",
                table: "MovieRating",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating",
                columns: new[] { "UserId", "RatingId" });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RatedById = table.Column<string>(nullable: true),
                    RatedOn = table.Column<DateTime>(nullable: false),
                    Score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_AspNetUsers_RatedById",
                        column: x => x.RatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_RatingId",
                table: "UserRating",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_RatingId1",
                table: "UserRating",
                column: "RatingId1");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRating_RatingId1",
                table: "MovieRating",
                column: "RatingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RatedById",
                table: "Rating",
                column: "RatedById");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_UserRating_RatingId",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_UserRating_RatingId1",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_MovieRating_RatingId1",
                table: "MovieRating");

            migrationBuilder.DropColumn(
                name: "RatingId1",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "RatingId1",
                table: "MovieRating");

            migrationBuilder.RenameColumn(
                name: "RatingId",
                table: "UserRating",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RatingId",
                table: "MovieRating",
                newName: "RatedById");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRating",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "RatedOn",
                table: "UserRating",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "UserRating",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RatedOn",
                table: "MovieRating",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "MovieRating",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieRating_AspNetUsers_MovieId",
                table: "MovieRating",
                column: "MovieId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_Id",
                table: "UserRating",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
