using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapiuppgift.Migrations
{
    public partial class Addedcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows");
        }
    }
}
