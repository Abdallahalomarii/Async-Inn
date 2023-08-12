using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AsyncInn.Migrations
{
    /// <inheritdoc />
    public partial class newRolesUpdatedandDeletedAnothers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "HotelRoom/Read", "property manager" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "ClaimValue",
                value: "HotelRoom/Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "ClaimValue",
                value: "HotelRoom/Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15,
                column: "ClaimValue",
                value: "Amenity/Read");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16,
                column: "ClaimValue",
                value: "AmenitiesToRoom/Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "HotelRoom/Read", "agent" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "HotelRoom/Update", "agent" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19,
                column: "ClaimValue",
                value: "AmenitiesToRoom/Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20,
                column: "ClaimValue",
                value: "AmenitiesToRoom/Delete");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 11, "permissions", "FullCRUD", "district manager" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "FullCRUD", "district manager" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "ClaimValue",
                value: "HotelRoom/Read");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "ClaimValue",
                value: "HotelRoom/Create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15,
                column: "ClaimValue",
                value: "HotelRoom/Update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16,
                column: "ClaimValue",
                value: "Amenity/Read");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Amenity/Create", "property manager" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Amenity/Update", "property manager" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19,
                column: "ClaimValue",
                value: "HotelRoom/Read");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20,
                column: "ClaimValue",
                value: "HotelRoom/Update");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 21, "permissions", "Amenity/Create", "agent" },
                    { 22, "permissions", "Amenity/Delete", "agent" }
                });
        }
    }
}
