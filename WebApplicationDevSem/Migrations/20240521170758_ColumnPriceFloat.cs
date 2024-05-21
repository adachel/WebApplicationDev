using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationDevSem.Migrations
{
    /// <inheritdoc />
    public partial class ColumnPriceFloat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "price",
                table: "products",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "decimal");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "productgroups",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "price",
                table: "products",
                type: "decimal",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "productgroups",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
