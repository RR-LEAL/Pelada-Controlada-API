using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeladaControladaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioCodigosOtp_scc_usuarios_UsuarioId1",
                table: "UsuarioCodigosOtp");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioCodigosOtp_UsuarioId1",
                table: "UsuarioCodigosOtp");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "UsuarioCodigosOtp");

            migrationBuilder.DropColumn(
                name: "codigo_recuperacao",
                table: "scc_usuarios");

            migrationBuilder.DropColumn(
                name: "validade_codigo",
                table: "scc_usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "UsuarioCodigosOtp",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "scc_usuarios",
                keyColumn: "email",
                keyValue: null,
                column: "email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "scc_usuarios",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCodigosOtp_UsuarioId",
                table: "UsuarioCodigosOtp",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioCodigosOtp_scc_usuarios_UsuarioId",
                table: "UsuarioCodigosOtp",
                column: "UsuarioId",
                principalTable: "scc_usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioCodigosOtp_scc_usuarios_UsuarioId",
                table: "UsuarioCodigosOtp");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioCodigosOtp_UsuarioId",
                table: "UsuarioCodigosOtp");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "UsuarioCodigosOtp",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "UsuarioCodigosOtp",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "scc_usuarios",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "codigo_recuperacao",
                table: "scc_usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "validade_codigo",
                table: "scc_usuarios",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCodigosOtp_UsuarioId1",
                table: "UsuarioCodigosOtp",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioCodigosOtp_scc_usuarios_UsuarioId1",
                table: "UsuarioCodigosOtp",
                column: "UsuarioId1",
                principalTable: "scc_usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
