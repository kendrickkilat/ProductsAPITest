using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPITest.Migrations
{
    public partial class addedNewPropertiesForEagerLoadingTest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pricings",
                newName: "PricingId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OrderItems",
                newName: "OrderItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PricingId",
                table: "Pricings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "OrderItems",
                newName: "id");
        }
    }
}
