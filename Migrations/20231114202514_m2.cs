using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinekraApi.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PerfumeName",
                table: "Perfumes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdersOrderId",
                table: "OrderDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Perfumes_BrandId",
                table: "Perfumes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrdersOrderId",
                table: "OrderDetails",
                column: "OrdersOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrdersOrderId",
                table: "OrderDetails",
                column: "OrdersOrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Perfumes_Brands_BrandId",
                table: "Perfumes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrdersOrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Perfumes_Brands_BrandId",
                table: "Perfumes");

            migrationBuilder.DropIndex(
                name: "IX_Perfumes_BrandId",
                table: "Perfumes");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrdersOrderId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrdersOrderId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "PerfumeName",
                table: "Perfumes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
