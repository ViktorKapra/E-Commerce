using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Data.Migrations
{
    /// <inheritdoc />
    public partial class Procut_adjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 100, "Action", false, null, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 200, "Adventure", false, null, 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 300, "RPG", false, null, 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 400, "Puzzle", false, null, 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 500, "Strategy", false, null, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 600, "Simulation", false, null, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 700, "Sports", false, null, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 800, "Racing", false, null, 4 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 900, "Horror", false, null, 4 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Background", "Count", "Genre", "IsDeleted", "Logo", "Rating" },
                values: new object[] { null, 1000, "MMO", false, null, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");
        }
    }
}
