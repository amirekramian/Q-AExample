using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6322), new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6322) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6300), new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6244), new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6244) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime", "UserID" },
                values: new object[] { new DateTime(2022, 7, 8, 4, 24, 37, 635, DateTimeKind.Local).AddTicks(2637), new DateTime(2022, 7, 8, 4, 24, 37, 635, DateTimeKind.Local).AddTicks(2637), 1 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 4, 24, 37, 635, DateTimeKind.Local).AddTicks(2538), new DateTime(2022, 7, 8, 4, 24, 37, 635, DateTimeKind.Local).AddTicks(2538) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 4, 24, 37, 635, DateTimeKind.Local).AddTicks(2452), new DateTime(2022, 7, 8, 4, 24, 37, 635, DateTimeKind.Local).AddTicks(2452) });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
