using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRent.Migrations
{
    public partial class AddedNewGuidToAllEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Class_ClassRef",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_ClassRef",
                table: "Car");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "User",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Reservation",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ClassId1",
                table: "Car",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Car",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Car_ClassId1",
                table: "Car",
                column: "ClassId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Class_ClassId1",
                table: "Car",
                column: "ClassId1",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Class_ClassId1",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_ClassId1",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ClassId1",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Car");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ClassRef",
                table: "Car",
                column: "ClassRef");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Class_ClassRef",
                table: "Car",
                column: "ClassRef",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
