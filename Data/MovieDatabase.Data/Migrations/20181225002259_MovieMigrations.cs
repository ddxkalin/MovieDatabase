using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class MovieMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId1",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId2",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "CastMember");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ApplicationUserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ApplicationUserId1",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ApplicationUserId2",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId2",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PosterImage",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "UserRating",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Actors");

            migrationBuilder.RenameColumn(
                name: "Music",
                table: "Movies",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "Director",
                table: "Movies",
                newName: "PosterImageLink");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CharacterName",
                table: "MovieActor",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Composers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Composers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieRating",
                columns: table => new
                {
                    MovieId = table.Column<string>(nullable: false),
                    RatedById = table.Column<string>(nullable: false),
                    RatedOn = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRating", x => new { x.RatedById, x.MovieId });
                    table.ForeignKey(
                        name: "FK_MovieRating_AspNetUsers_MovieId",
                        column: x => x.MovieId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieRating_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Screenwriters",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenwriters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOwnedMovie",
                columns: table => new
                {
                    MovieId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOwnedMovie", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UserOwnedMovie_AspNetUsers_MovieId",
                        column: x => x.MovieId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserOwnedMovie_Movies_UserId",
                        column: x => x.UserId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRating",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RatedById = table.Column<string>(nullable: false),
                    RatedOn = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRating", x => new { x.RatedById, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRating_AspNetUsers_RatedById",
                        column: x => x.RatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRating_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserWatchedMovie",
                columns: table => new
                {
                    MovieId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchedMovie", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UserWatchedMovie_AspNetUsers_MovieId",
                        column: x => x.MovieId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserWatchedMovie_Movies_UserId",
                        column: x => x.UserId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserWishlist",
                columns: table => new
                {
                    MovieId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWishlist", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UserWishlist_AspNetUsers_MovieId",
                        column: x => x.MovieId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserWishlist_Movies_UserId",
                        column: x => x.UserId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieComposer",
                columns: table => new
                {
                    MovieId = table.Column<string>(nullable: false),
                    MovieId1 = table.Column<string>(nullable: true),
                    ComposerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieComposer", x => new { x.MovieId, x.ComposerId });
                    table.ForeignKey(
                        name: "FK_MovieComposer_Composers_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Composers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieComposer_Movies_MovieId1",
                        column: x => x.MovieId1,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieDirector",
                columns: table => new
                {
                    MovieId = table.Column<string>(nullable: false),
                    MovieId1 = table.Column<string>(nullable: true),
                    DirectorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDirector", x => new { x.MovieId, x.DirectorId });
                    table.ForeignKey(
                        name: "FK_MovieDirector_Directors_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieDirector_Movies_MovieId1",
                        column: x => x.MovieId1,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieScreenwriter",
                columns: table => new
                {
                    MovieId = table.Column<string>(nullable: false),
                    MovieId1 = table.Column<string>(nullable: true),
                    ScreenwriterId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieScreenwriter", x => new { x.MovieId, x.ScreenwriterId });
                    table.ForeignKey(
                        name: "FK_MovieScreenwriter_Screenwriters_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Screenwriters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieScreenwriter_Movies_MovieId1",
                        column: x => x.MovieId1,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Slug",
                table: "Movies",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Composers_IsDeleted",
                table: "Composers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_IsDeleted",
                table: "Directors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MovieComposer_MovieId",
                table: "MovieComposer",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieComposer_MovieId1",
                table: "MovieComposer",
                column: "MovieId1",
                unique: true,
                filter: "[MovieId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDirector_MovieId",
                table: "MovieDirector",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDirector_MovieId1",
                table: "MovieDirector",
                column: "MovieId1",
                unique: true,
                filter: "[MovieId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRating_MovieId",
                table: "MovieRating",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieScreenwriter_MovieId",
                table: "MovieScreenwriter",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieScreenwriter_MovieId1",
                table: "MovieScreenwriter",
                column: "MovieId1",
                unique: true,
                filter: "[MovieId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Screenwriters_IsDeleted",
                table: "Screenwriters",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserOwnedMovie_MovieId",
                table: "UserOwnedMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_UserId",
                table: "UserRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchedMovie_MovieId",
                table: "UserWatchedMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWishlist_MovieId",
                table: "UserWishlist",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieComposer");

            migrationBuilder.DropTable(
                name: "MovieDirector");

            migrationBuilder.DropTable(
                name: "MovieRating");

            migrationBuilder.DropTable(
                name: "MovieScreenwriter");

            migrationBuilder.DropTable(
                name: "UserOwnedMovie");

            migrationBuilder.DropTable(
                name: "UserRating");

            migrationBuilder.DropTable(
                name: "UserWatchedMovie");

            migrationBuilder.DropTable(
                name: "UserWishlist");

            migrationBuilder.DropTable(
                name: "Composers");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Screenwriters");

            migrationBuilder.DropIndex(
                name: "IX_Movies_Slug",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CharacterName",
                table: "MovieActor");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Movies",
                newName: "Music");

            migrationBuilder.RenameColumn(
                name: "PosterImageLink",
                table: "Movies",
                newName: "Director");

            migrationBuilder.AlterColumn<string>(
                name: "Music",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId2",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PosterImage",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Movies",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UserRating",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Actors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Actors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Actors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CastMember",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    MovieId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CastMember_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ApplicationUserId",
                table: "Movies",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ApplicationUserId1",
                table: "Movies",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ApplicationUserId2",
                table: "Movies",
                column: "ApplicationUserId2");

            migrationBuilder.CreateIndex(
                name: "IX_CastMember_IsDeleted",
                table: "CastMember",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CastMember_MovieId",
                table: "CastMember",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId",
                table: "Movies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId1",
                table: "Movies",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId2",
                table: "Movies",
                column: "ApplicationUserId2",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
