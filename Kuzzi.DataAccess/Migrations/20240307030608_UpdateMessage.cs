using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kuzzi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReplyTo",
                table: "Message",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "replyTo",
                table: "Message",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_replyTo",
                table: "Message",
                column: "replyTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Message_replyTo",
                table: "Message",
                column: "replyTo",
                principalTable: "Message",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Message_replyTo",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_replyTo",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ReplyTo",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "replyTo",
                table: "Message");
        }
    }
}
