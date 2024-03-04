using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kuzzi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConversation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Conversation",
                keyColumn: "Id",
                keyValue: "7c18029b-160d-4ae8-a092-bc30c5d9cdaa");

            migrationBuilder.InsertData(
                table: "Conversation",
                columns: new[] { "Id", "ConversationType", "CreatedAt", "CreatedUserId", "LastUpdated" },
                values: new object[] { "25a1e0f1-e5c5-4751-9926-7be33b5cb429", null, new DateTime(2024, 3, 4, 9, 36, 2, 996, DateTimeKind.Utc).AddTicks(1250), null, new DateTime(2024, 3, 4, 9, 36, 2, 996, DateTimeKind.Utc).AddTicks(1250) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Conversation",
                keyColumn: "Id",
                keyValue: "25a1e0f1-e5c5-4751-9926-7be33b5cb429");

            migrationBuilder.InsertData(
                table: "Conversation",
                columns: new[] { "Id", "ConversationType", "CreatedAt", "CreatedUserId", "LastUpdated" },
                values: new object[] { "7c18029b-160d-4ae8-a092-bc30c5d9cdaa", null, new DateTime(2024, 3, 3, 14, 44, 48, 748, DateTimeKind.Utc).AddTicks(5930), null, new DateTime(2024, 3, 3, 14, 44, 48, 748, DateTimeKind.Utc).AddTicks(5930) });
        }
    }
}
