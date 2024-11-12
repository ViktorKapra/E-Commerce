﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalRating = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DateCreated",
                table: "Products",
                column: "DateCreated",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Platform",
                table: "Products",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TotalRating",
                table: "Products",
                column: "TotalRating",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}