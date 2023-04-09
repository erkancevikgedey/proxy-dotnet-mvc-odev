using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_proje_loginli.Data.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProxyBilgi");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "ProxyType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "ProxyType",
                columns: table => new
                {
                    ProxyTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProxyType", x => x.ProxyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ProxyBilgi",
                columns: table => new
                {
                    ProxyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProxyTipiProxyTypeId = table.Column<int>(type: "int", nullable: false),
                    UlkeCountryId = table.Column<int>(type: "int", nullable: false),
                    EklenmeZaman = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProxyBilgi", x => x.ProxyId);
                    table.ForeignKey(
                        name: "FK_ProxyBilgi_Country_UlkeCountryId",
                        column: x => x.UlkeCountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProxyBilgi_ProxyType_ProxyTipiProxyTypeId",
                        column: x => x.ProxyTipiProxyTypeId,
                        principalTable: "ProxyType",
                        principalColumn: "ProxyTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProxyBilgi_ProxyTipiProxyTypeId",
                table: "ProxyBilgi",
                column: "ProxyTipiProxyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProxyBilgi_UlkeCountryId",
                table: "ProxyBilgi",
                column: "UlkeCountryId");
        }
    }
}
