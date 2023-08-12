using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaAPI.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addparametersenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "Id", "Email", "Nome", "Senha" },
                values: new object[] { 1, "admin@logistica.com", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Ruas",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "A" });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Categoria", "DataArmazenamento", "ProdutoNome", "RuaId" },
                values: new object[] { 1, "Automotiva", new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), "Pneus", 1 });

            migrationBuilder.InsertData(
                table: "Localizacoes",
                columns: new[] { "Id", "Estante", "Posicao", "ProdutoId" },
                values: new object[] { 1, 10, 2, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ruas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Funcionarios");
        }
    }
}
