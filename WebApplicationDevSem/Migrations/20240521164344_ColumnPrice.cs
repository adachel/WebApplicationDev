using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationDevSem.Migrations
{
    /// <inheritdoc />
    public partial class ColumnPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "price",
                table: "products",
                type: "decimal",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "products");
        }
    }
}
