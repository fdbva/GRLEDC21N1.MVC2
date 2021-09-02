using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class IgnoreQtdLivrosPublicados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeLivrosPublicados",
                table: "Autores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantidadeLivrosPublicados",
                table: "Autores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
