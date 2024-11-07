using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace StoreService.Api.CarritoCompra.Migrations
{
    /// <inheritdoc />
    public partial class MigrationMySqlInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarritoSesions",
                columns: table => new
                {
                    CarritoSesionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSesions", x => x.CarritoSesionId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarritoSesionDetalles",
                columns: table => new
                {
                    CarritoSesionDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProductoSeleccionado = table.Column<string>(type: "longtext", nullable: true),
                    CarritoSesionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSesionDetalles", x => x.CarritoSesionDetalleId);
                    table.ForeignKey(
                        name: "FK_CarritoSesionDetalles_CarritoSesions_CarritoSesionId",
                        column: x => x.CarritoSesionId,
                        principalTable: "CarritoSesions",
                        principalColumn: "CarritoSesionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoSesionDetalles_CarritoSesionId",
                table: "CarritoSesionDetalles",
                column: "CarritoSesionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoSesionDetalles");

            migrationBuilder.DropTable(
                name: "CarritoSesions");
        }
    }
}
