using Microsoft.EntityFrameworkCore.Migrations;

namespace CookieFactory.Data.Migrations
{
    public partial class Cookie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cookie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookieFlavor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chips = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Filling = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topping = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cookie", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cookie");
        }
    }
}
