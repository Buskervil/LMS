using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.Courses.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemsTpc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_CourseItem_Id",
                table: "Article");

            migrationBuilder.DropTable(
                name: "CourseItem");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseSectionId",
                table: "Article",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Article",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Article",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PreviousItemId",
                table: "Article",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_CourseSectionId",
                table: "Article",
                column: "CourseSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_CourseSection_CourseSectionId",
                table: "Article",
                column: "CourseSectionId",
                principalTable: "CourseSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_CourseSection_CourseSectionId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_CourseSectionId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "CourseSectionId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "PreviousItemId",
                table: "Article");

            migrationBuilder.CreateTable(
                name: "CourseItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseSectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PreviousItemId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseItem_CourseSection_CourseSectionId",
                        column: x => x.CourseSectionId,
                        principalTable: "CourseSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseItem_CourseSectionId",
                table: "CourseItem",
                column: "CourseSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_CourseItem_Id",
                table: "Article",
                column: "Id",
                principalTable: "CourseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
