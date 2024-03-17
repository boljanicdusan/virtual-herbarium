using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualHerbarium.Backend.Migrations
{
    public partial class Added_Plant_Locations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LokacijeBiljaka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staniste = table.Column<string>(nullable: true),
                    Mjesto = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    BiljkaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LokacijeBiljaka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LokacijeBiljaka_Biljke_BiljkaId",
                        column: x => x.BiljkaId,
                        principalTable: "Biljke",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LokacijeBiljaka_BiljkaId",
                table: "LokacijeBiljaka",
                column: "BiljkaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LokacijeBiljaka");
        }
    }
}
