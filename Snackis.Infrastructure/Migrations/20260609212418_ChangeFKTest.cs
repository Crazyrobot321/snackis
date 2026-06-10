using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snackis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFKTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporterId",
                table: "UserReports");

            migrationBuilder.DropIndex(
                name: "IX_UserReports_ReporterId",
                table: "UserReports");

            migrationBuilder.DropColumn(
                name: "ReporterId",
                table: "UserReports");

            migrationBuilder.AlterColumn<string>(
                name: "ReporterUserId",
                table: "UserReports",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ReporterUserId",
                table: "UserReports",
                column: "ReporterUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporterUserId",
                table: "UserReports",
                column: "ReporterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporterUserId",
                table: "UserReports");

            migrationBuilder.DropIndex(
                name: "IX_UserReports_ReporterUserId",
                table: "UserReports");

            migrationBuilder.AlterColumn<string>(
                name: "ReporterUserId",
                table: "UserReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReporterId",
                table: "UserReports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ReporterId",
                table: "UserReports",
                column: "ReporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporterId",
                table: "UserReports",
                column: "ReporterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
