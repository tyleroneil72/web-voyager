using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_voyager.Migrations
{
    /// <inheritdoc />
    public partial class NewUserBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_GuestUsers_GuestUserId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "GuestUserId",
                schema: "Identity",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "Identity",
                table: "Bookings",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ApplicationUserId",
                schema: "Identity",
                table: "Bookings",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_GuestUsers_GuestUserId",
                schema: "Identity",
                table: "Bookings",
                column: "GuestUserId",
                principalSchema: "Identity",
                principalTable: "GuestUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_User_ApplicationUserId",
                schema: "Identity",
                table: "Bookings",
                column: "ApplicationUserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_GuestUsers_GuestUserId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_User_ApplicationUserId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ApplicationUserId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "GuestUserId",
                schema: "Identity",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_GuestUsers_GuestUserId",
                schema: "Identity",
                table: "Bookings",
                column: "GuestUserId",
                principalSchema: "Identity",
                principalTable: "GuestUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
