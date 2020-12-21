using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultoriaApplication.Migrations
{
    public partial class Addusuarioqueconcluiu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserConcludeId",
                table: "Card",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_UserConcludeId",
                table: "Card",
                column: "UserConcludeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_AspNetUsers_UserConcludeId",
                table: "Card",
                column: "UserConcludeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_AspNetUsers_UserConcludeId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_UserConcludeId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "UserConcludeId",
                table: "Card");
        }
    }
}
