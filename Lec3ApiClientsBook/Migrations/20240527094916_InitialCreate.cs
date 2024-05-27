using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lec3ApiClientsBook.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientbooks",
                columns: table => new
                {
                    bookId = table.Column<Guid>(type: "uuid", nullable: false),
                    clientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("book_pkey", x => x.bookId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientbooks");
        }
    }
}
