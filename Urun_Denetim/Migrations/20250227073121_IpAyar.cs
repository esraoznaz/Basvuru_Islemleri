using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urun_Denetim.Migrations
{
    /// <inheritdoc />
    public partial class IpAyar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAdresi",
                table: "KullaniciLoglaris",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAdresi",
                table: "KullaniciLoglaris");
        }
    }
}
