using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("2c592b78-10fb-4f31-b925-bc0a60794306"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("6ac0f273-92b1-4d4e-b5cb-c0474279d697"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("9f9242f8-f6f9-403d-81be-57f15801a47f"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("7211c09c-6268-43bd-b72d-612298ee9026"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("7245cfb3-2071-4143-b8f2-3b131bd405f4"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("814b2e91-7e7d-435d-9a9d-8c83e8bc7da9"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("7041c8d9-aa88-4e71-9ce6-422092c52b58"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("7e54bae7-d7d0-4382-96e6-911db1157514"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("7f957594-dc3d-491a-b21f-dcae12b314e9"));

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

            migrationBuilder.InsertData(
                table: "Birds",
                columns: new[] { "Id", "CanFly", "Name" },
                values: new object[,]
                {
                    { new Guid("1ab23ee7-de9e-4fe0-aaa9-0b3f8a580d19"), false, "Simon" },
                    { new Guid("a30a64d7-553f-4a8e-9617-8a70d5464cdb"), false, "Drake" },
                    { new Guid("b05cf71b-68a3-4515-b9d3-5b5b82026c6c"), false, "Gustav" }
                });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "LikesToPlay", "Name" },
                values: new object[,]
                {
                    { new Guid("24703e2b-2c40-493a-aa8c-dff44f4bc2e1"), false, "Nemo" },
                    { new Guid("4c1ee71a-22d4-483e-a471-092f1b334560"), false, "Simba" },
                    { new Guid("a872f013-48d7-4242-b6d1-45f01b2f5a15"), false, "Doris" }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3184fa5e-ff22-449a-893b-f1a500a136a6"), "Alfred" },
                    { new Guid("79d17d29-03d6-4b4a-8ece-c557e257f36b"), "Björn" },
                    { new Guid("f0e1b9a9-8f1d-40e6-bcdb-529ea5b7acae"), "Patrik" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("1ab23ee7-de9e-4fe0-aaa9-0b3f8a580d19"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("a30a64d7-553f-4a8e-9617-8a70d5464cdb"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("b05cf71b-68a3-4515-b9d3-5b5b82026c6c"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("24703e2b-2c40-493a-aa8c-dff44f4bc2e1"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("4c1ee71a-22d4-483e-a471-092f1b334560"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("a872f013-48d7-4242-b6d1-45f01b2f5a15"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("3184fa5e-ff22-449a-893b-f1a500a136a6"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("79d17d29-03d6-4b4a-8ece-c557e257f36b"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("f0e1b9a9-8f1d-40e6-bcdb-529ea5b7acae"));

            migrationBuilder.InsertData(
                table: "Birds",
                columns: new[] { "Id", "CanFly", "Name" },
                values: new object[,]
                {
                    { new Guid("2c592b78-10fb-4f31-b925-bc0a60794306"), false, "Drake" },
                    { new Guid("6ac0f273-92b1-4d4e-b5cb-c0474279d697"), false, "Gustav" },
                    { new Guid("9f9242f8-f6f9-403d-81be-57f15801a47f"), false, "Simon" }
                });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "LikesToPlay", "Name" },
                values: new object[,]
                {
                    { new Guid("7211c09c-6268-43bd-b72d-612298ee9026"), false, "Nemo" },
                    { new Guid("7245cfb3-2071-4143-b8f2-3b131bd405f4"), false, "Simba" },
                    { new Guid("814b2e91-7e7d-435d-9a9d-8c83e8bc7da9"), false, "Doris" }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7041c8d9-aa88-4e71-9ce6-422092c52b58"), "Patrik" },
                    { new Guid("7e54bae7-d7d0-4382-96e6-911db1157514"), "Björn" },
                    { new Guid("7f957594-dc3d-491a-b21f-dcae12b314e9"), "Alfred" }
                });
        }
    }
}
