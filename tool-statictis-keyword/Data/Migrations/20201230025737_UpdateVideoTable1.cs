using Microsoft.EntityFrameworkCore.Migrations;

namespace tool_statictis_keyword.Data.Migrations
{
    public partial class UpdateVideoTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLive",
                table: "Video");

            migrationBuilder.AlterColumn<string>(
                name: "ChannelId",
                table: "Video",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "Video",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLive",
                table: "Video",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f88",
                column: "ConcurrencyStamp",
                value: "faff44cd-f89d-4763-a510-3f64ac2e870b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f89",
                column: "ConcurrencyStamp",
                value: "10a7150a-5527-44c3-867c-44a0676a2369");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f90",
                column: "ConcurrencyStamp",
                value: "72fb00e6-c79f-4c25-b935-003a137510f3");
        }
    }
}
