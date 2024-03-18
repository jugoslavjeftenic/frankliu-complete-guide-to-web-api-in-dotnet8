using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T056_WebApiSwagger.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToShit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<string>(
		        name: "Description",
		        table: "Shirts",
		        nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shirts");
        }
    }
}
