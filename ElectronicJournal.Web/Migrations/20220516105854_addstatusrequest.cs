using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectronicJournal.Web.Migrations
{
    public partial class addstatusrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "CourseRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CourseRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestStatusId",
                table: "CourseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.Id);
                    table.UniqueConstraint("AK_RequestStatus_RequestStatusId", x => x.RequestStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequests_RequestStatusId",
                table: "CourseRequests",
                column: "RequestStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRequests_RequestStatus_RequestStatusId",
                table: "CourseRequests",
                column: "RequestStatusId",
                principalTable: "RequestStatus",
                principalColumn: "RequestStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRequests_RequestStatus_RequestStatusId",
                table: "CourseRequests");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropIndex(
                name: "IX_CourseRequests_RequestStatusId",
                table: "CourseRequests");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CourseRequests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CourseRequests");

            migrationBuilder.DropColumn(
                name: "RequestStatusId",
                table: "CourseRequests");
        }
    }
}
