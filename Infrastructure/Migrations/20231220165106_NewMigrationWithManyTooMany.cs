using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationWithManyTooMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AnimalModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CanFly = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LikesToPlay = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalModel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPassword = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAnimals",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AnimalModelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimals", x => new { x.UserId, x.AnimalModelId });
                    table.ForeignKey(
                        name: "FK_UserAnimals_AnimalModel_AnimalModelId",
                        column: x => x.AnimalModelId,
                        principalTable: "AnimalModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnimals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Discriminator", "Name" },
                values: new object[] { new Guid("005080d8-52ef-4649-8b5c-c12d053c5293"), "Dog", "Björn" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Discriminator", "LikesToPlay", "Name" },
                values: new object[] { new Guid("0336b8b4-ee19-4dc8-88bd-f608c44f4687"), "Cat", false, "Nemo" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Discriminator", "Name" },
                values: new object[] { new Guid("4fec0bb0-22e6-4e7f-b001-b046a92c8d36"), "Dog", "Alfred" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Discriminator", "LikesToPlay", "Name" },
                values: new object[,]
                {
                    { new Guid("53017fb1-1479-467f-8167-ee8d2d4bfd04"), "Cat", false, "Simba" },
                    { new Guid("6b347677-450e-4b1c-8a09-1b99baddcc50"), "Cat", false, "Doris" }
                });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "CanFly", "Discriminator", "Name" },
                values: new object[] { new Guid("8eb6be95-f91b-4ef0-8e3f-c46116a775f9"), false, "Bird", "Drake" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Discriminator", "Name" },
                values: new object[] { new Guid("9522ef88-2b59-4d1a-b087-82e704b3e612"), "Dog", "Patrik" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "CanFly", "Discriminator", "Name" },
                values: new object[,]
                {
                    { new Guid("b256e0b0-1543-4609-9fb5-01008e7b52c3"), false, "Bird", "Simon" },
                    { new Guid("df7fdee9-539f-4878-940b-c0d53cfdf60e"), false, "Bird", "Gustav" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimals_AnimalModelId",
                table: "UserAnimals",
                column: "AnimalModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnimals");

            migrationBuilder.DropTable(
                name: "AnimalModel");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
