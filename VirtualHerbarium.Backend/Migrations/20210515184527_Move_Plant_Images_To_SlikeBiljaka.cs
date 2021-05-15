using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualHerbarium.Backend.Migrations
{
    public partial class Move_Plant_Images_To_SlikeBiljaka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO SlikeBiljaka (BiljkaId, Slika, UPrirodi) 
                SELECT 
                    Id AS BiljkaId,	
                    Slika,
                    UPrirodi = 0
                FROM Biljke
                WHERE Slika is not null AND Slika <> ''

                INSERT INTO SlikeBiljaka (BiljkaId, Slika, UPrirodi) 
                SELECT 
                    Id AS BiljkaId,	
                    SlikaUPrirodi AS Slika,
                    UPrirodi = 1
                FROM Biljke
                WHERE SlikaUPrirodi is not null AND SlikaUPrirodi <> ''
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM SlikeBiljaka");
        }
    }
}
