using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnioSA.Sales.API.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arquivo",
                columns: table => new
                {
                    CodArquivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NomeArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arq = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Extensao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TamanhoArquivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivo", x => x.CodArquivo);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Documento = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Documento);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    CodPedido = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Documento = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    RazaoSocial = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    CEP = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Produto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumPedido = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prazo = table.Column<int>(type: "int", nullable: true),
                    Frete = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Regiao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorProduto = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.CodPedido);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    CodProduto = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.CodProduto);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivo");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
