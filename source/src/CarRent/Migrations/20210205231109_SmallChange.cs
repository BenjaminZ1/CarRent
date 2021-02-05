using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRent.Migrations
{
    public partial class SmallChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Class_ClassRef",
                table: "Reservation");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Class_ClassRef",
                table: "Reservation",
                column: "ClassRef",
                principalTable: "Class",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Class_ClassRef",
                table: "Reservation");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Class_ClassRef",
                table: "Reservation",
                column: "ClassRef",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
