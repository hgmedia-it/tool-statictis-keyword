using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tool_statictis_keyword.Data.Migrations
{
    public partial class CreateAllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Date",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false)
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
                name: "Keyword",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keyword_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Keyword_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statictis",
                columns: table => new
                {
                    KeywordId = table.Column<int>(nullable: false),
                    DateId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    SearchByDayResultId = table.Column<int>(nullable: false)
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
                        name: "FK_Statictis_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    VideoId = table.Column<string>(nullable: true),
                    ChannelName = table.Column<string>(nullable: true),
                    ViewCount = table.Column<int>(nullable: false),
                    ChannelId = table.Column<int>(nullable: false),
                    StatictisDateId = table.Column<int>(nullable: true),
                    StatictisKeywordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Video_Statictis_StatictisKeywordId_StatictisDateId",
                        columns: x => new { x.StatictisKeywordId, x.StatictisDateId },
                        principalTable: "Statictis",
                        principalColumns: new[] { "KeywordId", "DateId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchByDayResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    VideoPublishCount = table.Column<int>(nullable: false),
                    VideoLiveCount = table.Column<int>(nullable: false),
                    VideoMostPopularId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchByDayResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchByDayResults_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SearchByDayResults_Video_VideoMostPopularId",
                        column: x => x.VideoMostPopularId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ec864316-078a-406e-9013-f5e3d20d1f88", "c80fe514-85c0-418d-9632-e16e7a135516", "admin", "ADMIN" },
                    { "ec864316-078a-406e-9013-f5e3d20d1f89", "25cf0637-de29-4292-9ea9-351631551069", "manager", "MANAGER" },
                    { "ec864316-078a-406e-9013-f5e3d20d1f90", "e0928ea2-c4f9-4c88-972c-594109084ee9", "staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "92ad4f43-4be7-4fb8-909f-ced532c58461", 0, "90016888-7668-4a47-9050-de70b8aa621b", "admin@gmail.com", true, "Ad", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEDjtxkKmq1HdWDIebT4dygGnXFswCLC1irkWgM0FQ3K5mCnKLFfLYRmA8Q6W9r+z4w==", "0359038319", true, "64QW72XRQWP5FI2IWOZV3ZD6ILSBV4W2", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "92ad4f43-4be7-4fb8-909f-ced532c58461", "ec864316-078a-406e-9013-f5e3d20d1f88" });

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                table: "Category",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Date_UserId",
                table: "Date",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_CategoryId",
                table: "Keyword",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_UserId",
                table: "Keyword",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchByDayResults_UserId",
                table: "SearchByDayResults",
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

            migrationBuilder.CreateIndex(
                name: "IX_Video_UserId",
                table: "Video",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_StatictisKeywordId_StatictisDateId",
                table: "Video",
                columns: new[] { "StatictisKeywordId", "StatictisDateId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Statictis_SearchByDayResults_SearchByDayResultId",
                table: "Statictis",
                column: "SearchByDayResultId",
                principalTable: "SearchByDayResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keyword_Category_CategoryId",
                table: "Keyword");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchByDayResults_Video_VideoMostPopularId",
                table: "SearchByDayResults");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Statictis");

            migrationBuilder.DropTable(
                name: "Date");

            migrationBuilder.DropTable(
                name: "Keyword");

            migrationBuilder.DropTable(
                name: "SearchByDayResults");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f90");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "92ad4f43-4be7-4fb8-909f-ced532c58461", "ec864316-078a-406e-9013-f5e3d20d1f88" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec864316-078a-406e-9013-f5e3d20d1f88");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "92ad4f43-4be7-4fb8-909f-ced532c58461");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
