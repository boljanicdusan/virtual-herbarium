using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualHerbarium.Backend.Migrations
{
    public partial class Seed_Plant_Location_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO LokacijeBiljaka (Staniste, Mjesto, Latitude, Longitude, BiljkaId)
                SELECT Staniste, Mjesto, Latitude, Longitude, Id
                FROM Biljke");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
