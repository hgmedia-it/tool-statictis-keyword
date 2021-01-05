using Microsoft.EntityFrameworkCore.Migrations;

namespace tool_statictis_keyword.Data.Migrations
{
    public partial class UpdateVideoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keyword_Campaign_CampaignId1",
                table: "Keyword");

            migrationBuilder.DropIndex(
                name: "IX_Keyword_CampaignId1",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "CampaignId1",
                table: "Keyword");

            migrationBuilder.AlterColumn<int>(
                name: "CampaignId",
                table: "Keyword",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_CampaignId",
                table: "Keyword",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keyword_Campaign_CampaignId",
                table: "Keyword",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keyword_Campaign_CampaignId",
                table: "Keyword");

            migrationBuilder.DropIndex(
                name: "IX_Keyword_CampaignId",
                table: "Keyword");

            migrationBuilder.AlterColumn<string>(
                name: "CampaignId",
                table: "Keyword",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CampaignId1",
                table: "Keyword",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f88",
                column: "ConcurrencyStamp",
                value: "5c11d6b4-8f8c-4cf1-a928-35d92caf68a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f89",
                column: "ConcurrencyStamp",
                value: "715be976-08bf-4a4d-9eba-6b0f5a77b7b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f90",
                column: "ConcurrencyStamp",
                value: "83445a87-ffd4-4571-b066-d97471055d23");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_CampaignId1",
                table: "Keyword",
                column: "CampaignId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Keyword_Campaign_CampaignId1",
                table: "Keyword",
                column: "CampaignId1",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
