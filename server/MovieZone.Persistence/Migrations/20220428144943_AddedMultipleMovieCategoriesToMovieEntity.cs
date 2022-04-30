#nullable disable

namespace MovieZone.Persistence.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedMultipleMovieCategoriesToMovieEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MoviesCategories_MoviesCategoryId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MoviesCategoryId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MoviesCategoryId",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "MovieMoviesCategory",
                columns: table => new
                {
                    MoviesCategoriesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MoviesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieMoviesCategory", x => new { x.MoviesCategoriesId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_MovieMoviesCategory_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieMoviesCategory_MoviesCategories_MoviesCategoriesId",
                        column: x => x.MoviesCategoriesId,
                        principalTable: "MoviesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieMoviesCategory_MoviesId",
                table: "MovieMoviesCategory",
                column: "MoviesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieMoviesCategory");

            migrationBuilder.AddColumn<string>(
                name: "MoviesCategoryId",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MoviesCategoryId",
                table: "Movies",
                column: "MoviesCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MoviesCategories_MoviesCategoryId",
                table: "Movies",
                column: "MoviesCategoryId",
                principalTable: "MoviesCategories",
                principalColumn: "Id");
        }
    }
}
