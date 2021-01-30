using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRent.Migrations
{
    public partial class renameTableCarSpecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarSpecification_Car_CarRef",
                table: "CarSpecification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarSpecification",
                table: "CarSpecification");

            migrationBuilder.RenameTable(
                name: "CarSpecification",
                newName: "Specification");

            migrationBuilder.RenameIndex(
                name: "IX_CarSpecification_CarRef",
                table: "Specification",
                newName: "IX_Specification_CarRef");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specification",
                table: "Specification",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specification_Car_CarRef",
                table: "Specification",
                column: "CarRef",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specification_Car_CarRef",
                table: "Specification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specification",
                table: "Specification");

            migrationBuilder.RenameTable(
                name: "Specification",
                newName: "CarSpecification");

            migrationBuilder.RenameIndex(
                name: "IX_Specification_CarRef",
                table: "CarSpecification",
                newName: "IX_CarSpecification_CarRef");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarSpecification",
                table: "CarSpecification",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarSpecification_Car_CarRef",
                table: "CarSpecification",
                column: "CarRef",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
