using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathEliteAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSchollStageInGradeClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchoolStage",
                table: "Grades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolStage",
                table: "Grades");
        }
    }
}
