using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sync.API.Persistance.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ColaArticulos",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<string>(maxLength: 1000, nullable: false),
                    SKU = table.Column<string>(maxLength: 1000, nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    FechaActualizacion = table.Column<DateTime>(nullable: false),
                    Sincronizado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColaArticulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orden",
                schema: "dbo",
                columns: table => new
                {
                    OrdenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroOrden = table.Column<long>(nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalImpuestos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaOrden = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orden", x => x.OrdenId);
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrdenId = table.Column<int>(nullable: false),
                    SKU = table.Column<string>(maxLength: 1000, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalImpuestos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nombre = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulos_Orden",
                        column: x => x.OrdenId,
                        principalSchema: "dbo",
                        principalTable: "Orden",
                        principalColumn: "OrdenId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrdenId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 1000, nullable: false),
                    Apellido = table.Column<string>(maxLength: 1000, nullable: false),
                    Email = table.Column<string>(maxLength: 1000, nullable: false),
                    Telefono = table.Column<string>(maxLength: 1000, nullable: false),
                    Direccion = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Orden",
                        column: x => x.OrdenId,
                        principalSchema: "dbo",
                        principalTable: "Orden",
                        principalColumn: "OrdenId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_OrdenId",
                schema: "dbo",
                table: "Articulos",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_OrdenId",
                schema: "dbo",
                table: "Cliente",
                column: "OrdenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articulos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cliente",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ColaArticulos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Orden",
                schema: "dbo");
        }
    }
}
