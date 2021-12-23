using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SewaAPI.Migrations
{
    public partial class satu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_T_Account",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_Account", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_Mobil",
                columns: table => new
                {
                    MobilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusMobil = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_Mobil", x => x.MobilId);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_Penyewa",
                columns: table => new
                {
                    PenyewaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alamat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoTelp = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_Penyewa", x => x.PenyewaId);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_LogPenyewa",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PenyewaId = table.Column<int>(type: "int", nullable: false),
                    MobilId = table.Column<int>(type: "int", nullable: false),
                    MulaiSewa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AkhirSewa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TglKembali = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_LogPenyewa", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_Tb_T_LogPenyewa_Tb_T_Mobil_MobilId",
                        column: x => x.MobilId,
                        principalTable: "Tb_T_Mobil",
                        principalColumn: "MobilId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_T_LogPenyewa_Tb_T_Penyewa_PenyewaId",
                        column: x => x.PenyewaId,
                        principalTable: "Tb_T_Penyewa",
                        principalColumn: "PenyewaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_Account_Email",
                table: "Tb_T_Account",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_LogPenyewa_MobilId",
                table: "Tb_T_LogPenyewa",
                column: "MobilId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_LogPenyewa_PenyewaId",
                table: "Tb_T_LogPenyewa",
                column: "PenyewaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_Penyewa_NoTelp",
                table: "Tb_T_Penyewa",
                column: "NoTelp",
                unique: true,
                filter: "[NoTelp] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_T_Account");

            migrationBuilder.DropTable(
                name: "Tb_T_LogPenyewa");

            migrationBuilder.DropTable(
                name: "Tb_T_Mobil");

            migrationBuilder.DropTable(
                name: "Tb_T_Penyewa");
        }
    }
}
