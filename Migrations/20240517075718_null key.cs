using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SverigesFordonsFörening.Migrations
{
    /// <inheritdoc />
    public partial class nullkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cars_FkCarId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Motorcycles_FkMotorcycleId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "FkMotorcycleId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FkCarId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cars_FkCarId",
                table: "Orders",
                column: "FkCarId",
                principalTable: "Cars",
                principalColumn: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Motorcycles_FkMotorcycleId",
                table: "Orders",
                column: "FkMotorcycleId",
                principalTable: "Motorcycles",
                principalColumn: "MotorcycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cars_FkCarId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Motorcycles_FkMotorcycleId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "FkMotorcycleId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FkCarId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cars_FkCarId",
                table: "Orders",
                column: "FkCarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Motorcycles_FkMotorcycleId",
                table: "Orders",
                column: "FkMotorcycleId",
                principalTable: "Motorcycles",
                principalColumn: "MotorcycleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
