using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_proje_loginli.Data.Migrations
{
    public partial class test6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WebsiteText",
                table: "WebsiteText");

            migrationBuilder.AlterColumn<string>(
                name: "MainPageText",
                table: "WebsiteText",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "WebId",
                table: "WebsiteText",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProxyTypeId",
                table: "BotAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebsiteText",
                table: "WebsiteText",
                column: "WebId");

            migrationBuilder.CreateIndex(
                name: "IX_BotAddress_ProxyTypeId",
                table: "BotAddress",
                column: "ProxyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BotAddress_ProxyType_ProxyTypeId",
                table: "BotAddress",
                column: "ProxyTypeId",
                principalTable: "ProxyType",
                principalColumn: "ProxyTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotAddress_ProxyType_ProxyTypeId",
                table: "BotAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WebsiteText",
                table: "WebsiteText");

            migrationBuilder.DropIndex(
                name: "IX_BotAddress_ProxyTypeId",
                table: "BotAddress");

            migrationBuilder.DropColumn(
                name: "WebId",
                table: "WebsiteText");

            migrationBuilder.DropColumn(
                name: "ProxyTypeId",
                table: "BotAddress");

            migrationBuilder.AlterColumn<string>(
                name: "MainPageText",
                table: "WebsiteText",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebsiteText",
                table: "WebsiteText",
                column: "MainPageText");
        }
    }
}
