using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minimal_Api.Migrations
{
    /// <inheritdoc />
    public partial class RenomearTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_veiculos",
                table: "veiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admnistradores",
                table: "Admnistradores");

            migrationBuilder.RenameTable(
                name: "veiculos",
                newName: "Veiculos");

            migrationBuilder.RenameTable(
                name: "Admnistradores",
                newName: "Administradores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculos",
                table: "Veiculos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administradores",
                table: "Administradores",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculos",
                table: "Veiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administradores",
                table: "Administradores");

            migrationBuilder.RenameTable(
                name: "Veiculos",
                newName: "veiculos");

            migrationBuilder.RenameTable(
                name: "Administradores",
                newName: "Admnistradores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_veiculos",
                table: "veiculos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admnistradores",
                table: "Admnistradores",
                column: "Id");
        }
    }
}
