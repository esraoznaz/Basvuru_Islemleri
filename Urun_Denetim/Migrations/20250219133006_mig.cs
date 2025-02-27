using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urun_Denetim.Migrations
{
    public partial class mig : Migration
    {
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
                    AdminYetki = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminses", x => x.AdminID);
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
                name: "Kategoris",
                columns: table => new
                {
                    KategoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoris", x => x.KategoriID);
                    table.ForeignKey(
                        name: "FK_Kategoris_Markas_MarkaID",
                        column: x => x.MarkaID,
                        principalTable: "Markas",
                        principalColumn: "MarkaID",
                        onDelete: ReferentialAction.Restrict); // Kaskad silmeyi devre dışı bırak
                });

            migrationBuilder.CreateTable(
                name: "Urunlers",
                columns: table => new
                {
                    UrunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                        onDelete: ReferentialAction.Restrict); // Kaskad silmeyi devre dışı bırak
                    table.ForeignKey(
                        name: "FK_Urunlers_Markas_MarkaID",
                        column: x => x.MarkaID,
                        principalTable: "Markas",
                        principalColumn: "MarkaID",
                        onDelete: ReferentialAction.Restrict); // Kaskad silmeyi devre dışı bırak
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kategoris_MarkaID",
                table: "Kategoris",
                column: "MarkaID");

            migrationBuilder.CreateIndex(
                name: "IX_Urunlers_KategoriID",
                table: "Urunlers",
                column: "KategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_Urunlers_MarkaID",
                table: "Urunlers",
                column: "MarkaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adminses");

            migrationBuilder.DropTable(
                name: "Urunlers");

            migrationBuilder.DropTable(
                name: "Kategoris");

            migrationBuilder.DropTable(
                name: "Markas");
        }
    }
}
