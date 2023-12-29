using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class user5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Customer_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "AspNetUsers",
                newName: "cust_id");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CustomerId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_cust_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Customer_cust_id",
                table: "AspNetUsers",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Customer_cust_id",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "cust_id",
                table: "AspNetUsers",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_cust_id",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Customer_CustomerId",
                table: "AspNetUsers",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "cust_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
