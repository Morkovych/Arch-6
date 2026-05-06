using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "IsActive", "LastName", "RegistrationDate" },
                values: new object[,]
                {
                    { new Guid("03881404-8bad-4a92-aa4a-6576d9d4aa0f"), new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "george-orwell@gmail.ru", "George", true, "Orwell", new DateTime(2026, 4, 29, 11, 4, 14, 5, DateTimeKind.Utc).AddTicks(911) },
                    { new Guid("dcf3c08f-006f-4c04-94d7-d9920df8a34e"), new DateTime(1952, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "robert-martin@gmail.ru", "Robert", true, "Martin", new DateTime(2026, 4, 29, 11, 4, 14, 5, DateTimeKind.Utc).AddTicks(905) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
