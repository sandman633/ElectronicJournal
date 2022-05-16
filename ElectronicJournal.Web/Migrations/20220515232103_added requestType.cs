using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectronicJournal.Web.Migrations
{
    public partial class addedrequestType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "CourseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequests_TypeId",
                table: "CourseRequests",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRequests_CourseTypes_TypeId",
                table: "CourseRequests",
                column: "TypeId",
                principalTable: "CourseTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRequests_CourseTypes_TypeId",
                table: "CourseRequests");

            migrationBuilder.DropIndex(
                name: "IX_CourseRequests_TypeId",
                table: "CourseRequests");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "CourseRequests");
        }
    }
}
