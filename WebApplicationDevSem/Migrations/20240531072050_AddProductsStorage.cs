using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationDevSem.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStorage_products_ProductId",
                table: "ProductStorage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStorage_storages_StorageId",
                table: "ProductStorage");

            migrationBuilder.RenameTable(
                name: "ProductStorage",
                newName: "ProductsStorage");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStorage_StorageId",
                table: "ProductsStorage",
                newName: "IX_ProductsStorage_StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsStorage_products_ProductId",
                table: "ProductsStorage",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsStorage_storages_StorageId",
                table: "ProductsStorage",
                column: "StorageId",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsStorage_products_ProductId",
                table: "ProductsStorage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsStorage_storages_StorageId",
                table: "ProductsStorage");

            migrationBuilder.RenameTable(
                name: "ProductsStorage",
                newName: "ProductStorage");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsStorage_StorageId",
                table: "ProductStorage",
                newName: "IX_ProductStorage_StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStorage_products_ProductId",
                table: "ProductStorage",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStorage_storages_StorageId",
                table: "ProductStorage",
                column: "StorageId",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
