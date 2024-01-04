using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class cart6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Delivery_Info_delivery_id1",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Delivery_Info");

            migrationBuilder.DropIndex(
                name: "IX_Order_delivery_id1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "delivery_id1",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "delivery_id1",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Delivery_Info",
                columns: table => new
                {
                    delivery_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery_Info", x => x.delivery_id);
                });

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
