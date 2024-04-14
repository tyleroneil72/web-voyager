using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_voyager.Migrations
{
    /// <inheritdoc />
    public partial class NewReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewableType",
                schema: "Identity",
                table: "Reviews",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewableType",
                schema: "Identity",
                table: "Reviews");
        }
    }
}
