using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualHerbarium.Backend.Migrations
{
    public partial class Added_SlikeBiljaka_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlikeBiljaka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slika = table.Column<string>(nullable: true),
                    UPrirodi = table.Column<bool>(nullable: false),
                    BiljkaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlikeBiljaka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlikeBiljaka_Biljke_BiljkaId",
                        column: x => x.BiljkaId,
                        principalTable: "Biljke",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlikeBiljaka_BiljkaId",
                table: "SlikeBiljaka",
                column: "BiljkaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlikeBiljaka");
        }
    }
}
