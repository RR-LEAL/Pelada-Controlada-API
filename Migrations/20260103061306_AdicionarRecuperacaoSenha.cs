using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeladaControladaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarRecuperacaoSenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codigo_recuperacao",
                table: "scc_usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "telefone",
                table: "scc_usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "validade_codigo",
                table: "scc_usuarios",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigo_recuperacao",
                table: "scc_usuarios");

            migrationBuilder.DropColumn(
                name: "telefone",
                table: "scc_usuarios");

            migrationBuilder.DropColumn(
                name: "validade_codigo",
                table: "scc_usuarios");
        }
    }
}
