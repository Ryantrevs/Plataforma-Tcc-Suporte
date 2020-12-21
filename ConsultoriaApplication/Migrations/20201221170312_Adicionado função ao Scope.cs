using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultoriaApplication.Migrations
{
    public partial class AdicionadofunçãoaoScope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Function",
                table: "UserScope",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Function",
                table: "UserScope");
        }
    }
}
