using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToysAndGamesDataAccess.Migrations
{
    public partial class createProductCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AgeRestriction = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageBase64 = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.CheckConstraint("CK_Products_AgeRestriction_Range", "(AgeRestriction >= 0 AND AgeRestriction <= 100)");
                    table.CheckConstraint("CK_Products_Price_Range", "(Price >= 1 AND Price <= 1000)");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeRestriction", "Company", "Description", "ImageBase64", "ImagePath", "Name", "Price" },
                values: new object[] { 1, 3, "HotWheels", "Diecast cars", new byte[] { 0 }, "", "HotWheels Car", 25.00m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
