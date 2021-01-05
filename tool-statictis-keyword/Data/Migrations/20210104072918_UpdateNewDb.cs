using Microsoft.EntityFrameworkCore.Migrations;

namespace tool_statictis_keyword.Data.Migrations
{
    public partial class UpdateNewDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f88",
                column: "ConcurrencyStamp",
                value: "079bf895-e88c-4540-95ef-9476bd5fdd50");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f89",
                column: "ConcurrencyStamp",
                value: "16847635-d19a-422c-8734-7cb20c73573d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f90",
                column: "ConcurrencyStamp",
                value: "213cff5d-0d57-4736-aacb-3267e49ab344");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
