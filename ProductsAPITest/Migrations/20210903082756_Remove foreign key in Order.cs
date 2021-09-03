using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPITest.Migrations
{
    public partial class RemoveforeignkeyinOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderItemId",
                table: "Orders",
                nullable: false,
                defaultValue: "");
        }
    }
}
