using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class cart9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Delivery_Info_delivery_id1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_delivery_id1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "delivery_id1",
                table: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Order_cust_id",
                table: "Order",
                column: "cust_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_delivery_id",
                table: "Order",
                column: "delivery_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_discount_id",
                table: "Order",
                column: "discount_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_cust_id",
                table: "Order",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Delivery_Info_delivery_id",
                table: "Order",
                column: "delivery_id",
                principalTable: "Delivery_Info",
                principalColumn: "delivery_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Discount_discount_id",
                table: "Order",
                column: "discount_id",
                principalTable: "Discount",
                principalColumn: "discount_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_cust_id",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Delivery_Info_delivery_id",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Discount_discount_id",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_cust_id",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_delivery_id",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_discount_id",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "delivery_id1",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_delivery_id1",
                table: "Order",
                column: "delivery_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Delivery_Info_delivery_id1",
                table: "Order",
                column: "delivery_id1",
                principalTable: "Delivery_Info",
                principalColumn: "delivery_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
