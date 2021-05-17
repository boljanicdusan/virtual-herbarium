using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualHerbarium.Backend.Migrations
{
    public partial class Slika_Removed_From_Plants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Biljke");

            migrationBuilder.DropColumn(
                name: "SlikaUPrirodi",
                table: "Biljke");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Biljke",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SlikaUPrirodi",
                table: "Biljke",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
