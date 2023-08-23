using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5W5_Cour_revision.Data.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Chat",
                columns: new[] { "Id", "ImageUrl", "Nom" },
                values: new object[] { 1, "https://img.freepik.com/free-photo/red-white-cat-i-white-studio_155003-13189.jpg?w=2000", "Rosie" });

            migrationBuilder.InsertData(
                table: "Chat",
                columns: new[] { "Id", "ImageUrl", "Nom" },
                values: new object[] { 2, "https://images.saymedia-content.com/.image/t_share/MTc0OTY4MDk2OTIxMTY3ODQw/bicolor-patterns-in-cats.jpg", "Channel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat");
        }
    }
}
