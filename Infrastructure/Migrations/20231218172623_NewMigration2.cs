using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("21e2cb9a-c71e-49f7-af64-3e9f38f3ccaf"), false, "Simon" },
                    { new Guid("6b7e2976-1004-4cdc-8c09-b21131846a0a"), false, "Gustav" },
                    { new Guid("c38239de-4ff1-467d-9f87-9e2fe1818465"), false, "Drake" }
                });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "LikesToPlay", "Name" },
                values: new object[,]
                {
                    { new Guid("446a0d4c-0a54-4e16-b812-c712ae312de9"), false, "Nemo" },
                    { new Guid("91094fb7-1176-4a2f-9b2b-2b0d9a1fdc46"), false, "Doris" },
                    { new Guid("b810711b-69d9-4388-baf8-25099da34d6d"), false, "Simba" }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10f13ad9-0f72-4206-b626-783d0b95c1ea"), "Alfred" },
                    { new Guid("6f18bd58-efe4-4882-8161-c22b71e404c2"), "Patrik" },
                    { new Guid("884fc3b6-7fd1-4851-ad44-62b1196ab8ac"), "Björn" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("21e2cb9a-c71e-49f7-af64-3e9f38f3ccaf"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("6b7e2976-1004-4cdc-8c09-b21131846a0a"));

            migrationBuilder.DeleteData(
                table: "Birds",
                keyColumn: "Id",
                keyValue: new Guid("c38239de-4ff1-467d-9f87-9e2fe1818465"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("446a0d4c-0a54-4e16-b812-c712ae312de9"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("91094fb7-1176-4a2f-9b2b-2b0d9a1fdc46"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("b810711b-69d9-4388-baf8-25099da34d6d"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("10f13ad9-0f72-4206-b626-783d0b95c1ea"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("6f18bd58-efe4-4882-8161-c22b71e404c2"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("884fc3b6-7fd1-4851-ad44-62b1196ab8ac"));

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
    }
}
