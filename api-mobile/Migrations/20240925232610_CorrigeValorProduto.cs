using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_mobile.Migrations
{
    /// <inheritdoc />
    public partial class CorrigeValorProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorPago",
                table: "Produtos",
                newName: "Valor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Produtos",
                newName: "ValorPago");
        }
    }
}
