using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(141), new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(141) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(117), new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(56), new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(56) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4383), new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4383) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4362), new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4362) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4303), new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4303) });
        }
    }
}
