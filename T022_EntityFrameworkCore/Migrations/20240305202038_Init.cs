using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T022_EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shirts",
                columns: new[] { "ShirtId", "Brand", "Color", "Gender", "Price", "Size" },
                values: new object[,]
                {
                    { 1, "My Brand", "Blue", "Men", 30.0, 10 },
                    { 2, "My Brand", "Black", "Men", 35.0, 12 },
                    { 3, "Your Brand", "Pink", "Women", 28.0, 8 },
                    { 4, "Your Brand", "Yello", "Women", 30.0, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shirts",
                keyColumn: "ShirtId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shirts",
                keyColumn: "ShirtId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shirts",
                keyColumn: "ShirtId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Shirts",
                keyColumn: "ShirtId",
                keyValue: 4);
        }
    }
}
