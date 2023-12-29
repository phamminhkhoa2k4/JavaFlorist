using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class User1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Customercust_id",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    cust_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    P_no = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.cust_id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Customer_Customercust_id",
                table: "Wishlist");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_Customercust_id",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "Customercust_id",
                table: "Wishlist");
        }
    }
}
