using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoMobileProject.Data.Migrations
{
    public partial class UpdateCarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Carses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carses_UserId",
                table: "Carses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carses_AspNetUsers_UserId",
                table: "Carses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carses_AspNetUsers_UserId",
                table: "Carses");

            migrationBuilder.DropIndex(
                name: "IX_Carses_UserId",
                table: "Carses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carses");
        }
    }
}
