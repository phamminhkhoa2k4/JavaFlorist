using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class cart3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delivery_Info",
                columns: table => new
                {
                    delivery_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery_Info", x => x.delivery_id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cust_id = table.Column<int>(type: "int", nullable: false),
                    delivery_id = table.Column<int>(type: "int", nullable: false),
                    delivery_id1 = table.Column<int>(type: "int", nullable: false),
                    discount_id = table.Column<int>(type: "int", nullable: false),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_Order_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Delivery_Info_delivery_id1",
                        column: x => x.delivery_id1,
                        principalTable: "Delivery_Info",
                        principalColumn: "delivery_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartId",
                table: "Order",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_delivery_id1",
                table: "Order",
                column: "delivery_id1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Delivery_Info");
        }
    }
}
