using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class cart7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Occasion_id",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "delivery_id1",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubTotal",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Delivery_Info",
                columns: table => new
                {
                    delivery_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery_Info", x => x.delivery_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_delivery_id1",
                table: "Order",
                column: "delivery_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Occasion_id",
                table: "Order",
                column: "Occasion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Delivery_Info_delivery_id1",
                table: "Order",
                column: "delivery_id1",
                principalTable: "Delivery_Info",
                principalColumn: "delivery_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Occassion_Occasion_id",
                table: "Order",
                column: "Occasion_id",
                principalTable: "Occassion",
                principalColumn: "Occasion_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Delivery_Info_delivery_id1",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Occassion_Occasion_id",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Delivery_Info");

            migrationBuilder.DropIndex(
                name: "IX_Order_delivery_id1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_Occasion_id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Occasion_id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "delivery_id1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "CartItems");
        }
    }
}
