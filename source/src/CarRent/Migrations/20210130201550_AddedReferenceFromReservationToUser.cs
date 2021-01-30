using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRent.Migrations
{
    public partial class AddedReferenceFromReservationToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRef",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserRef",
                table: "Reservation",
                column: "UserRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_UserRef",
                table: "Reservation",
                column: "UserRef",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_UserRef",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_UserRef",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserRef",
                table: "Reservation");
        }
    }
}
