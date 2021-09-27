using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZombieSurvival.Core.Migrations
{
    public partial class CreatePriceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Survivors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Survivors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Inventories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Inventories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Name", "Point", "Quantity" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 9, 27, 2, 55, 13, 890, DateTimeKind.Local).AddTicks(6786), null, "Water", 4, 1 },
                    { 2L, new DateTime(2021, 9, 27, 2, 55, 13, 891, DateTimeKind.Local).AddTicks(4262), null, "Food", 3, 1 },
                    { 3L, new DateTime(2021, 9, 27, 2, 55, 13, 891, DateTimeKind.Local).AddTicks(4275), null, "Medication", 2, 1 },
                    { 4L, new DateTime(2021, 9, 27, 2, 55, 13, 891, DateTimeKind.Local).AddTicks(4278), null, "Ammunition", 1, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Survivors");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Survivors");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Inventories");
        }
    }
}
