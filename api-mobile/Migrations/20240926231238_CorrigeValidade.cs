using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_mobile.Migrations
{
    /// <inheritdoc />
    public partial class CorrigeValidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Validades",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Validades");
        }
    }
}
