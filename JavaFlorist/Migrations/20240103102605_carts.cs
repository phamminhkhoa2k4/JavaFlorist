using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class carts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Bouquet_Info_bouquet_id",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_bouquet_id",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "bouquet_id1",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_bouquet_id1",
                table: "CartItems",
                column: "bouquet_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Bouquet_Info_bouquet_id1",
                table: "CartItems",
                column: "bouquet_id1",
                principalTable: "Bouquet_Info",
                principalColumn: "bouquet_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Bouquet_Info_bouquet_id1",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_bouquet_id1",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "bouquet_id1",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_bouquet_id",
                table: "CartItems",
                column: "bouquet_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Bouquet_Info_bouquet_id",
                table: "CartItems",
                column: "bouquet_id",
                principalTable: "Bouquet_Info",
                principalColumn: "bouquet_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
