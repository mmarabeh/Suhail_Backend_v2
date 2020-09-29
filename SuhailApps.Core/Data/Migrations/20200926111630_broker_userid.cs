using Microsoft.EntityFrameworkCore.Migrations;

namespace SuhailApps.Core.Data.Migrations
{
    public partial class broker_userid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brokers_ApplicationUsers_UserId1",
                table: "Brokers");

            migrationBuilder.DropIndex(
                name: "IX_Brokers_UserId1",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Brokers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_UserId",
                table: "Brokers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brokers_ApplicationUsers_UserId",
                table: "Brokers",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brokers_ApplicationUsers_UserId",
                table: "Brokers");

            migrationBuilder.DropIndex(
                name: "IX_Brokers_UserId",
                table: "Brokers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Brokers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Brokers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_UserId1",
                table: "Brokers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Brokers_ApplicationUsers_UserId1",
                table: "Brokers",
                column: "UserId1",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
