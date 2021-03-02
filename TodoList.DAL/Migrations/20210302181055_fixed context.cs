using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.DAL.Migrations
{
    public partial class fixedcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserDalId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserDalId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserDalId",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserDalId",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserDalId",
                table: "Tasks",
                column: "UserDalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserDalId",
                table: "Tasks",
                column: "UserDalId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
