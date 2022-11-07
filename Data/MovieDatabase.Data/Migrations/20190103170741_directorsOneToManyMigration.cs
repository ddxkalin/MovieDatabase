using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class directorsOneToManyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieComposer");

            migrationBuilder.DropTable(
                name: "MovieDirector");

            migrationBuilder.DropTable(
                name: "MovieScreenwriter");

            migrationBuilder.AddColumn<string>(
                name: "ComposerId",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DirectorId",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScreenwriterId",
                table: "Movies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ComposerId",
                table: "Movies",
                column: "ComposerId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ScreenwriterId",
                table: "Movies",
                column: "ScreenwriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Composers_ComposerId",
                table: "Movies",
                column: "ComposerId",
                principalTable: "Composers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Screenwriters_ScreenwriterId",
                table: "Movies",
                column: "ScreenwriterId",
                principalTable: "Screenwriters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Composers_ComposerId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Screenwriters_ScreenwriterId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ComposerId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ScreenwriterId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ComposerId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ScreenwriterId",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "MovieComposer",
                columns: table => new
                {
                    MovieId = table.Column<string>(nullable: false),
                    ComposerId = table.Column<string>(nullable: false),
                    MovieId1 = table.Column<string>(nullable: true)
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
                    DirectorId = table.Column<string>(nullable: false),
                    MovieId1 = table.Column<string>(nullable: true)
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
                    ScreenwriterId = table.Column<string>(nullable: false),
                    MovieId1 = table.Column<string>(nullable: true)
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
                name: "IX_MovieScreenwriter_MovieId",
                table: "MovieScreenwriter",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieScreenwriter_MovieId1",
                table: "MovieScreenwriter",
                column: "MovieId1",
                unique: true,
                filter: "[MovieId1] IS NOT NULL");
        }
    }
}
