using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECom.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "Name", "Platform", "Price", "TotalRating" },
                values: new object[,]
                {
                    { 1, new DateOnly(2021, 1, 1), "PC game 1", 1, 50m, 4.5m },
                    { 2, new DateOnly(2021, 1, 2), "PC game 2", 1, 60m, 4.6m },
                    { 3, new DateOnly(2021, 1, 3), "PC game 3", 1, 70m, 4.7m },
                    { 4, new DateOnly(2018, 1, 4), "Mobile game 1", 2, 30m, 4.8m },
                    { 5, new DateOnly(2021, 1, 5), "Mobile game 2", 2, 40m, 4.9m },
                    { 6, new DateOnly(2020, 1, 6), "Mobile game 3", 2, 50m, 5.0m },
                    { 7, new DateOnly(2021, 1, 7), "Console game 1", 0, 60m, 4.1m },
                    { 8, new DateOnly(2021, 1, 8), "Console game 2", 0, 70m, 4.2m },
                    { 9, new DateOnly(2021, 1, 9), "VR game 1", 3, 80m, 4.3m },
                    { 10, new DateOnly(2023, 1, 10), "Web game 1", 4, 90m, 4.4m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
