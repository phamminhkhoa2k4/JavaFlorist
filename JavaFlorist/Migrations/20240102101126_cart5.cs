using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class cart5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Delivery_Info_DeliveryInfodelivery_id",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "DeliveryInfodelivery_id",
                table: "Order",
                newName: "delivery_id1");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryInfodelivery_id",
                table: "Order",
                newName: "IX_Order_delivery_id1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Delivery_Info",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Delivery_Info",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Delivery_Info_delivery_id1",
                table: "Order",
                column: "delivery_id1",
                principalTable: "Delivery_Info",
                principalColumn: "delivery_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Delivery_Info_delivery_id1",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "delivery_id1",
                table: "Order",
                newName: "DeliveryInfodelivery_id");

            migrationBuilder.RenameIndex(
                name: "IX_Order_delivery_id1",
                table: "Order",
                newName: "IX_Order_DeliveryInfodelivery_id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Delivery_Info",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Delivery_Info",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Delivery_Info_DeliveryInfodelivery_id",
                table: "Order",
                column: "DeliveryInfodelivery_id",
                principalTable: "Delivery_Info",
                principalColumn: "delivery_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
