using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToysAndGamesDataAccess.Migrations
{
    public partial class changeImageBase64toImageBytes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageBase64",
                table: "Products",
                newName: "ImageBytes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageBytes",
                table: "Products",
                newName: "ImageBase64");
        }
    }
}
