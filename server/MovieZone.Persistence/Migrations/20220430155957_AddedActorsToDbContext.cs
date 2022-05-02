#nullable disable

namespace MovieZone.Persistence.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedActorsToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovie_Actor_ActorsId",
                table: "ActorMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Actors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actors",
                table: "Actors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovie_Actors_ActorsId",
                table: "ActorMovie",
                column: "ActorsId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovie_Actors_ActorsId",
                table: "ActorMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actors",
                table: "Actors");

            migrationBuilder.RenameTable(
                name: "Actors",
                newName: "Actor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovie_Actor_ActorsId",
                table: "ActorMovie",
                column: "ActorsId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
