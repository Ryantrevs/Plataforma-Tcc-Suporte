using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultoriaApplication.Migrations
{
    public partial class AdicionadofunçãoaouserTaskList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Function",
                table: "UserTasklist",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Function",
                table: "UserTasklist");
        }
    }
}
