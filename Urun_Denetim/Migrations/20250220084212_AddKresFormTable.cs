using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urun_Denetim.Migrations
{
    /// <inheritdoc />
    public partial class AddKresFormTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategoris_Markas_MarkaID",
                table: "Kategoris");

            migrationBuilder.AlterColumn<int>(
                name: "MarkaID",
                table: "Kategoris",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategoris_Markas_MarkaID",
                table: "Kategoris",
                column: "MarkaID",
                principalTable: "Markas",
                principalColumn: "MarkaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategoris_Markas_MarkaID",
                table: "Kategoris");

            migrationBuilder.AlterColumn<int>(
                name: "MarkaID",
                table: "Kategoris",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kategoris_Markas_MarkaID",
                table: "Kategoris",
                column: "MarkaID",
                principalTable: "Markas",
                principalColumn: "MarkaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
