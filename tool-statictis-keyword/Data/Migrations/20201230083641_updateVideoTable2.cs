using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tool_statictis_keyword.Data.Migrations
{
    public partial class updateVideoTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishDate",
                table: "Video",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f88",
                column: "ConcurrencyStamp",
                value: "8650f134-1dde-4ca1-bd7a-533929f40669");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f89",
                column: "ConcurrencyStamp",
                value: "794e2b5f-8ad9-4d8b-ac74-425bc08dbf3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f90",
                column: "ConcurrencyStamp",
                value: "d58fb49f-fd9e-4aca-b79e-480a091b7a58");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishDate",
                table: "Video",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f88",
                column: "ConcurrencyStamp",
                value: "247e4b79-76b2-451c-96b6-a5e0fbdbd1ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f89",
                column: "ConcurrencyStamp",
                value: "6b430d4e-5063-458a-9421-773ee735ed3c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f90",
                column: "ConcurrencyStamp",
                value: "3762c41a-bf64-4234-945d-f7207ed33db8");
        }
    }
}
