using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinekraApi.Migrations
{
    public partial class mg3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PerfumeId",
                table: "OrderDetails",
                column: "PerfumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Perfumes_PerfumeId",
                table: "OrderDetails",
                column: "PerfumeId",
                principalTable: "Perfumes",
                principalColumn: "PerfumeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Perfumes_PerfumeId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_PerfumeId",
                table: "OrderDetails");
        }
    }
}
