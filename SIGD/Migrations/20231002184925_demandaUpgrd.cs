using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGD.Migrations
{
    /// <inheritdoc />
    public partial class demandaUpgrd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataVecimento",
                table: "Demandas");

            migrationBuilder.RenameColumn(
                name: "demandante",
                table: "Demandas",
                newName: "Demandante");

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCadastro",
                table: "Demandas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Prazo",
                table: "Demandas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtCadastro",
                table: "Demandas");

            migrationBuilder.DropColumn(
                name: "Prazo",
                table: "Demandas");

            migrationBuilder.RenameColumn(
                name: "Demandante",
                table: "Demandas",
                newName: "demandante");

            migrationBuilder.AddColumn<string>(
                name: "DataVecimento",
                table: "Demandas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
