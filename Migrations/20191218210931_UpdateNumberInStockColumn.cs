using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryAllTheThings.Migrations
{
    public partial class UpdateNumberInStockColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockId",
                table: "InventoryItems");

            migrationBuilder.AddColumn<int>(
                name: "NumberInStock",
                table: "InventoryItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberInStock",
                table: "InventoryItems");

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "InventoryItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
