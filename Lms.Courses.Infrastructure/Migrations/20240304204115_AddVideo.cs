using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.Courses.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CourseSectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PreviousItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    ContentLink = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_CourseSection_CourseSectionId",
                        column: x => x.CourseSectionId,
                        principalTable: "CourseSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Video_CourseSectionId",
                table: "Video",
                column: "CourseSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Video");
        }
    }
}
