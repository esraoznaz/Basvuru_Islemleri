using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urun_Denetim.Migrations
{
    /// <inheritdoc />
    public partial class yeniEklemeler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SonucAciklama",
                table: "KresForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "dagıtım",
                table: "KresForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "durumu",
                table: "KresForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "isturu",
                table: "KresForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SonucAciklama",
                table: "KresForms");

            migrationBuilder.DropColumn(
                name: "dagıtım",
                table: "KresForms");

            migrationBuilder.DropColumn(
                name: "durumu",
                table: "KresForms");

            migrationBuilder.DropColumn(
                name: "isturu",
                table: "KresForms");
        }
    }
}
