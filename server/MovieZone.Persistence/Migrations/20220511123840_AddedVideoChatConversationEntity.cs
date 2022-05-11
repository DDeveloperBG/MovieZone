#nullable disable

namespace MovieZone.Persistence.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedVideoChatConversationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoChatConversations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoChatConversations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserVideoChatConversation",
                columns: table => new
                {
                    ParticipantsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoChatConversationsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserVideoChatConversation", x => new { x.ParticipantsId, x.VideoChatConversationsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserVideoChatConversation_Users_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserVideoChatConversation_VideoChatConversations_VideoChatConversationsId",
                        column: x => x.VideoChatConversationsId,
                        principalTable: "VideoChatConversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserVideoChatConversation_VideoChatConversationsId",
                table: "ApplicationUserVideoChatConversation",
                column: "VideoChatConversationsId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoChatConversations_IsDeleted",
                table: "VideoChatConversations",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserVideoChatConversation");

            migrationBuilder.DropTable(
                name: "VideoChatConversations");
        }
    }
}
