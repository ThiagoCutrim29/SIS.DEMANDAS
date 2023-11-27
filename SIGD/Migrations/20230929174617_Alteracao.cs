using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGD.Migrations
{
    /// <inheritdoc />
    public partial class Alteracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoUsuario",
                table: "Usuario",
                newName: "Senha");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Usuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Usuario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Perfil",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuario",
                newName: "TipoUsuario");
        }
    }
}
