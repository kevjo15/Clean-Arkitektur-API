using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
