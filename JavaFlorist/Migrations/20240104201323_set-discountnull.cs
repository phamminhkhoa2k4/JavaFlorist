using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class setdiscountnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Discount_discount_id",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "discount_id",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Discount_discount_id",
                table: "Order",
                column: "discount_id",
                principalTable: "Discount",
                principalColumn: "discount_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Discount_discount_id",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "discount_id",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Discount_discount_id",
                table: "Order",
                column: "discount_id",
                principalTable: "Discount",
                principalColumn: "discount_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
