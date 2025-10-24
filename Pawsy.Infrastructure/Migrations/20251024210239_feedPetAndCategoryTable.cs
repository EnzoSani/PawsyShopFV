using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pawsy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class feedPetAndCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Mascotas caninas", "Perros" },
                    { 2, "Mascotas felinas", "Gatos" }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "Age", "CategoryId", "Created_Date", "Description", "Gender", "ImageUrl", "Name", "Updated_Date" },
                values: new object[,]
                {
                    { 1, 2, 1, null, "Fusce tincidunt maximus leo, sed scelerisque massa auctor sit amet.", "Macho", "https://placehold.co/600x400", "Pupo", null },
                    { 2, 5, 1, null, "Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "Macho", "https://placehold.co/600x400", "Pipo", null },
                    { 3, 7, 2, null, "Fusce tincidunt maximus leo, sed scelerisque massa auctor sit amet.", "Macho", "https://placehold.co/600x400", "Tito", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
