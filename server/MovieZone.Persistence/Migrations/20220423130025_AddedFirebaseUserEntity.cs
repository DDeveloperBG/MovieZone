using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieZone.Persistence.Migrations
{
    public partial class AddedFirebaseUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirebaseUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FirebaseUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirebaseUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FirebaseUserId",
                table: "AspNetUsers",
                column: "FirebaseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FirebaseUser_FirebaseUserId",
                table: "AspNetUsers",
                column: "FirebaseUserId",
                principalTable: "FirebaseUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FirebaseUser_FirebaseUserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FirebaseUser");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FirebaseUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirebaseUserId",
                table: "AspNetUsers");
        }
    }
}
