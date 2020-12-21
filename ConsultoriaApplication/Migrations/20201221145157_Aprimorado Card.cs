using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultoriaApplication.Migrations
{
    public partial class AprimoradoCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Concluido",
                table: "Card",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFinal",
                table: "Card",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concluido",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "DataFinal",
                table: "Card");
        }
    }
}
