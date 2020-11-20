using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Data.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ModuleId",
                table: "AspNetUsers",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Modules_ModuleId",
                table: "AspNetUsers",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Modules_ModuleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ModuleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "AspNetUsers");
        }
    }
}
