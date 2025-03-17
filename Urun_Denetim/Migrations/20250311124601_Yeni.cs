using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urun_Denetim.Migrations
{
    /// <inheritdoc />
    public partial class Yeni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adminses",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminSifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminYetki = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminses", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Ilces",
                columns: table => new
                {
                    ilceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ilceAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilces", x => x.ilceId);
                });

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
                    isturu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    durumu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dagıtım = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SonucAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KresForms", x => x.KresFormId);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciLoglaris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IslemTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EndPoint = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Detay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciLoglaris", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Markas",
                columns: table => new
                {
                    MarkaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkaAd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markas", x => x.MarkaID);
                });

            migrationBuilder.CreateTable(
                name: "Mahalles",
                columns: table => new
                {
                    mahalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mahalleAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ilceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mahalles", x => x.mahalleId);
                    table.ForeignKey(
                        name: "FK_Mahalles_Ilces_ilceId",
                        column: x => x.ilceId,
                        principalTable: "Ilces",
                        principalColumn: "ilceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kategoris",
                columns: table => new
                {
                    KategoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoris", x => x.KategoriID);
                    table.ForeignKey(
                        name: "FK_Kategoris_Markas_MarkaID",
                        column: x => x.MarkaID,
                        principalTable: "Markas",
                        principalColumn: "MarkaID");
                });

            migrationBuilder.CreateTable(
                name: "Urunlers",
                columns: table => new
                {
                    UrunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunFiyat = table.Column<int>(type: "int", nullable: true),
                    UrunStok = table.Column<int>(type: "int", nullable: true),
                    KategoriID = table.Column<int>(type: "int", nullable: false),
                    MarkaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunlers", x => x.UrunID);
                    table.ForeignKey(
                        name: "FK_Urunlers_Kategoris_KategoriID",
                        column: x => x.KategoriID,
                        principalTable: "Kategoris",
                        principalColumn: "KategoriID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Urunlers_Markas_MarkaID",
                        column: x => x.MarkaID,
                        principalTable: "Markas",
                        principalColumn: "MarkaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kategoris_MarkaID",
                table: "Kategoris",
                column: "MarkaID");

            migrationBuilder.CreateIndex(
                name: "IX_Mahalles_ilceId",
                table: "Mahalles",
                column: "ilceId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunlers_KategoriID",
                table: "Urunlers",
                column: "KategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_Urunlers_MarkaID",
                table: "Urunlers",
                column: "MarkaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ilces");

            migrationBuilder.DropTable(
                name: "Mahalles");

            

            migrationBuilder.DropTable(
                name: "KresForms");

            migrationBuilder.DropTable(
                name: "KullaniciLoglaris");

            

            migrationBuilder.DropTable(
                name: "Urunlers");

            migrationBuilder.DropTable(
                name: "Adminses");

            migrationBuilder.DropTable(
                name: "Kategoris");

            migrationBuilder.DropTable(
                name: "Markas");
        }
    }
}
