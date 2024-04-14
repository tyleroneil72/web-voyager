using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_voyager.Migrations
{
    /// <inheritdoc />
    public partial class Reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_OldUs_OldUId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "OldUs",
                schema: "Identity");

            migrationBuilder.RenameColumn(
                name: "OldUId",
                schema: "Identity",
                table: "Bookings",
                newName: "GuestUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_OldUId",
                schema: "Identity",
                table: "Bookings",
                newName: "IX_Bookings_GuestUserId");

            migrationBuilder.CreateTable(
                name: "GuestUsers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "Identity",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReviewingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_GuestUsers_GuestUserId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "GuestUsers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "Identity");

            migrationBuilder.RenameColumn(
                name: "GuestUserId",
                schema: "Identity",
                table: "Bookings",
                newName: "OldUId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_GuestUserId",
                schema: "Identity",
                table: "Bookings",
                newName: "IX_Bookings_OldUId");

            migrationBuilder.CreateTable(
                name: "OldUs",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OldUs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_OldUs_OldUId",
                schema: "Identity",
                table: "Bookings",
                column: "OldUId",
                principalSchema: "Identity",
                principalTable: "OldUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
