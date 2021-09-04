using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class QtdPaginasLivro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtdPaginas",
                table: "Livros",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdPaginas",
                table: "Livros");
        }
    }
}
