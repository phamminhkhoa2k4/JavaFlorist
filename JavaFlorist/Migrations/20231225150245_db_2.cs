using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class db_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Customer_Customercust_id",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_Customercust_id",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "Customercust_id",
                table: "Wishlist");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Customercust_id",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_Customercust_id",
                table: "Wishlist",
                column: "Customercust_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Customer_Customercust_id",
                table: "Wishlist",
                column: "Customercust_id",
                principalTable: "Customer",
                principalColumn: "cust_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
