using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeladaControladaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioCodigoOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioCodigosOtp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiraEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Usado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCodigosOtp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioCodigosOtp_scc_usuarios_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "scc_usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCodigosOtp_UsuarioId1",
                table: "UsuarioCodigosOtp",
                column: "UsuarioId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioCodigosOtp");
        }
    }
}
