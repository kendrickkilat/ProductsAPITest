using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPITest.Migrations
{
    public partial class ChangedFloatToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Pricings",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Pricings",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "OrderItems",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
