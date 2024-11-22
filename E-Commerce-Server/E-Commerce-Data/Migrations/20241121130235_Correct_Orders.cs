using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Data.Migrations
{
    /// <inheritdoc />
    public partial class Correct_Orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderListId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Orders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                columns: new[] { "OrderListId", "ProductId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                columns: new[] { "Id", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderListId",
                table: "Orders",
                column: "OrderListId");
        }
    }
}
