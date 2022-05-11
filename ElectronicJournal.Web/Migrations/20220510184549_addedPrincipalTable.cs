using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectronicJournal.Web.Migrations
{
    public partial class addedPrincipalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrincipalId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Principal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principal", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrincipalId",
                table: "AspNetUsers",
                column: "PrincipalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Principal_PrincipalId",
                table: "AspNetUsers",
                column: "PrincipalId",
                principalTable: "Principal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Principal_PrincipalId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Principal");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PrincipalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrincipalId",
                table: "AspNetUsers");
        }
    }
}
