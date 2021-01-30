using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRent.Migrations
{
    public partial class renameTableCarClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarClass_ClassRef",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_CarClass_ClassRef",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarClass",
                table: "CarClass");

            migrationBuilder.RenameTable(
                name: "CarClass",
                newName: "Class");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Class",
                table: "Class",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Class_ClassRef",
                table: "Car",
                column: "ClassRef",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Class_ClassRef",
                table: "Reservation",
                column: "ClassRef",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Class_ClassRef",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Class_ClassRef",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Class",
                table: "Class");

            migrationBuilder.RenameTable(
                name: "Class",
                newName: "CarClass");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarClass",
                table: "CarClass",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarClass_ClassRef",
                table: "Car",
                column: "ClassRef",
                principalTable: "CarClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_CarClass_ClassRef",
                table: "Reservation",
                column: "ClassRef",
                principalTable: "CarClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
