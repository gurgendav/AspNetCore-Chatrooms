using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatrooms.Web.Api.Data.Migrations
{
    public partial class Identity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Chatrooms");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Chatrooms",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ChatMessages",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Chatrooms_CreatedById",
                table: "Chatrooms",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_CreatedById",
                table: "ChatMessages",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_AspNetUsers_CreatedById",
                table: "ChatMessages",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chatrooms_AspNetUsers_CreatedById",
                table: "Chatrooms",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_AspNetUsers_CreatedById",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Chatrooms_AspNetUsers_CreatedById",
                table: "Chatrooms");

            migrationBuilder.DropIndex(
                name: "IX_Chatrooms_CreatedById",
                table: "Chatrooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_CreatedById",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Chatrooms");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ChatMessages");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Chatrooms",
                nullable: false,
                defaultValue: 0);
        }
    }
}
