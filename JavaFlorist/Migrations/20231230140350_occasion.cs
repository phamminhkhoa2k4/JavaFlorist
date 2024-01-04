using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaFlorist.Migrations
{
    public partial class occasion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Occassion",
                columns: table => new
                {
                    Occasion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Occasion_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occassion", x => x.Occasion_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Occassion");
        }
    }
}
