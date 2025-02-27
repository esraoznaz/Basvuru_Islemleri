using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urun_Denetim.Migrations
{
    /// <inheritdoc />
    public partial class AddKresFormTableEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KresForms",
                columns: table => new
                {
                    KresFormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soyisim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    tc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ilce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mahalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KresForms", x => x.KresFormId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KresForms");
        }
    }
}
