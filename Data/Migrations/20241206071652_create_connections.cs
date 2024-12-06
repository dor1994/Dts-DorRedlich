using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class createconnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_QueueEntries_CustomerId",
                table: "QueueEntries",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueEntries_Users_CustomerId",
                table: "QueueEntries",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueEntries_Users_CustomerId",
                table: "QueueEntries");

            migrationBuilder.DropIndex(
                name: "IX_QueueEntries_CustomerId",
                table: "QueueEntries");
        }
    }
}
