using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDogAndCatBreedAndWeight : Migration
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
                    LikesToPlay = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Breed = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Dog_Breed = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dog_Weight = table.Column<int>(type: "int", nullable: true)
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
                columns: new[] { "Id", "CanFly", "Discriminator", "Name" },
                values: new object[] { new Guid("22f1b863-a4fa-46a2-b637-0e46aa14de6e"), false, "Bird", "Gustav" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("50c6d480-e2f1-4951-87ad-380886414108"), "Golden Retriver", "Dog", "Patrik", 50 });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "CanFly", "Discriminator", "Name" },
                values: new object[] { new Guid("8ae07781-1c2c-40a1-89bf-7d7832dd6095"), false, "Bird", "Simon" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("8fe661c4-68e3-4d7c-a500-a3429494e88f"), "Labrador", "Dog", "Björn", 20 });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "CanFly", "Discriminator", "Name" },
                values: new object[] { new Guid("902ce39e-ad27-4aea-84ee-9829c80a5957"), false, "Bird", "Drake" });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("9a270fa1-eab5-4463-8ced-5db94d130620"), "Beagle", "Dog", "Alfred", 15 });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Breed", "Discriminator", "LikesToPlay", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("a76dbfb5-71c0-4995-b956-343327c36053"), "Persian", "Cat", false, "Doris", 5 },
                    { new Guid("b566961d-d570-487e-ba51-fe718c9336b1"), "Siamese", "Cat", false, "Nemo", 6 },
                    { new Guid("d85bfc26-ab97-4a31-a96f-5291a2ffd7b4"), "BondeKatt", "Cat", false, "Simba", 10 }
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
