using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_voyager.Migrations
{
    /// <inheritdoc />
    public partial class CancelBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                schema: "Identity",
                table: "Bookings",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelled",
                schema: "Identity",
                table: "Bookings");
        }
    }
}
