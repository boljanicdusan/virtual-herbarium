using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualHerbarium.Backend.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biljke",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrsta = table.Column<string>(nullable: true),
                    Porodica = table.Column<string>(nullable: true),
                    Red = table.Column<string>(nullable: true),
                    TrivijalniNaziv = table.Column<string>(nullable: true),
                    Sinonim = table.Column<string>(nullable: true),
                    Staniste = table.Column<string>(nullable: true),
                    Mjesto = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    Slika = table.Column<string>(nullable: true),
                    SlikaUPrirodi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biljke", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biljke");
        }
    }
}
