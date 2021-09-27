using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZombieSurvival.Core.Migrations
{
    public partial class AddInventorySurvivorForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2021, 9, 27, 3, 53, 11, 389, DateTimeKind.Local).AddTicks(6568));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2021, 9, 27, 3, 53, 11, 390, DateTimeKind.Local).AddTicks(4233));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2021, 9, 27, 3, 53, 11, 390, DateTimeKind.Local).AddTicks(4247));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2021, 9, 27, 3, 53, 11, 390, DateTimeKind.Local).AddTicks(4250));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2021, 9, 27, 2, 55, 13, 890, DateTimeKind.Local).AddTicks(6786));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2021, 9, 27, 2, 55, 13, 891, DateTimeKind.Local).AddTicks(4262));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2021, 9, 27, 2, 55, 13, 891, DateTimeKind.Local).AddTicks(4275));

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2021, 9, 27, 2, 55, 13, 891, DateTimeKind.Local).AddTicks(4278));
        }
    }
}
