using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualHerbarium.Backend.Migrations
{
    public partial class Added_Lat_And_Long : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Biljke",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Biljke",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Biljke");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Biljke");
        }
    }
}
