using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tool_statictis_keyword.Data.Migrations
{
    public partial class UpdateNewDatabaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keyword_Category_CategoryId",
                table: "Keyword");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Statictis_StatictisKeywordId_StatictisDateId",
                table: "Video");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Statictis");

            migrationBuilder.DropTable(
                name: "Date");

            migrationBuilder.DropTable(
                name: "SearchByDayResults");

            migrationBuilder.DropIndex(
                name: "IX_Video_StatictisKeywordId_StatictisDateId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Keyword_CategoryId",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "StatictisDateId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "StatictisKeywordId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Keyword");

            migrationBuilder.AddColumn<bool>(
                name: "IsLive",
                table: "Video",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Video",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Video",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CampaignId",
                table: "Keyword",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampaignId1",
                table: "Keyword",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Keyword",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaign_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Keyword_Video",
                columns: table => new
                {
                    KeywordId = table.Column<int>(nullable: false),
                    VideoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword_Video", x => new { x.KeywordId, x.VideoId });
                    table.ForeignKey(
                        name: "FK_Keyword_Video_Keyword_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keyword",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Keyword_Video_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewsCountByDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ViewsCount = table.Column<float>(nullable: false),
                    VideoId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewsCountByDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewsCountByDay_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViewsCountByDay_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f88",
                column: "ConcurrencyStamp",
                value: "80bcef4d-fb92-4cae-803d-0b69c89b9010");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f89",
                column: "ConcurrencyStamp",
                value: "343298ad-27c0-4ebc-b5c3-431d0ec7c2f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f90",
                column: "ConcurrencyStamp",
                value: "86d6c74d-1761-4463-98ce-e82b4dcc33fe");

            migrationBuilder.CreateIndex(
                name: "IX_Video_UserId",
                table: "Video",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_CampaignId1",
                table: "Keyword",
                column: "CampaignId1");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_UserId",
                table: "Campaign",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_Video_VideoId",
                table: "Keyword_Video",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewsCountByDay_UserId",
                table: "ViewsCountByDay",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewsCountByDay_VideoId",
                table: "ViewsCountByDay",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keyword_Campaign_CampaignId1",
                table: "Keyword",
                column: "CampaignId1",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_UserId",
                table: "Video",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keyword_Campaign_CampaignId1",
                table: "Keyword");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_UserId",
                table: "Video");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Keyword_Video");

            migrationBuilder.DropTable(
                name: "ViewsCountByDay");

            migrationBuilder.DropIndex(
                name: "IX_Video_UserId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Keyword_CampaignId1",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "IsLive",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "CampaignId1",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Keyword");

            migrationBuilder.AddColumn<int>(
                name: "StatictisDateId",
                table: "Video",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatictisKeywordId",
                table: "Video",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Video",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Keyword",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Date",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Date", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Date_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchByDayResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoLiveCount = table.Column<int>(type: "int", nullable: false),
                    VideoMostPopularId = table.Column<int>(type: "int", nullable: false),
                    VideoPublishCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchByDayResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchByDayResults_Video_VideoMostPopularId",
                        column: x => x.VideoMostPopularId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statictis",
                columns: table => new
                {
                    KeywordId = table.Column<int>(type: "int", nullable: false),
                    DateId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    SearchByDayResultId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statictis", x => new { x.KeywordId, x.DateId });
                    table.ForeignKey(
                        name: "FK_Statictis_Date_DateId",
                        column: x => x.DateId,
                        principalTable: "Date",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statictis_Keyword_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keyword",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statictis_SearchByDayResults_SearchByDayResultId",
                        column: x => x.SearchByDayResultId,
                        principalTable: "SearchByDayResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statictis_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f88",
                column: "ConcurrencyStamp",
                value: "e75b0c28-a212-4425-a1c8-4961c72df49a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f89",
                column: "ConcurrencyStamp",
                value: "2e7cab66-6bfc-4a38-885b-a0b91ed84fb8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f90",
                column: "ConcurrencyStamp",
                value: "2a79baff-eefb-4cc1-8cab-961131a1721a");

            migrationBuilder.CreateIndex(
                name: "IX_Video_StatictisKeywordId_StatictisDateId",
                table: "Video",
                columns: new[] { "StatictisKeywordId", "StatictisDateId" });

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_CategoryId",
                table: "Keyword",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Date_UserId",
                table: "Date",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchByDayResults_VideoMostPopularId",
                table: "SearchByDayResults",
                column: "VideoMostPopularId");

            migrationBuilder.CreateIndex(
                name: "IX_Statictis_DateId",
                table: "Statictis",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_Statictis_SearchByDayResultId",
                table: "Statictis",
                column: "SearchByDayResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Statictis_UserId",
                table: "Statictis",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keyword_Category_CategoryId",
                table: "Keyword",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Statictis_StatictisKeywordId_StatictisDateId",
                table: "Video",
                columns: new[] { "StatictisKeywordId", "StatictisDateId" },
                principalTable: "Statictis",
                principalColumns: new[] { "KeywordId", "DateId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
