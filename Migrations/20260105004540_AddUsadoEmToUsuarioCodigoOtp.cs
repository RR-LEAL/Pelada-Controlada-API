using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeladaControladaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUsadoEmToUsuarioCodigoOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UsadoEm",
                table: "UsuarioCodigosOtp",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsadoEm",
                table: "UsuarioCodigosOtp");
        }
    }
}
