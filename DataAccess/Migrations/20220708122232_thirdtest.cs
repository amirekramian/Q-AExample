using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class thirdtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_UserID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostID",
                table: "Comments");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

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
                columns: new[] { "InsertedDateTime", "IsDeleted", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4362), null, new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4362) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4303), new DateTime(2022, 7, 8, 5, 22, 31, 767, DateTimeKind.Local).AddTicks(4303) });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserID",
                table: "Posts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_UserID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostID",
                table: "Comments");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

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
                columns: new[] { "InsertedDateTime", "IsDeleted", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6300), false, new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "InsertedDateTime", "UpdatedDateTime" },
                values: new object[] { new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6244), new DateTime(2022, 7, 8, 4, 55, 40, 442, DateTimeKind.Local).AddTicks(6244) });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserID",
                table: "Posts",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID",
                unique: true);
        }
    }
}
