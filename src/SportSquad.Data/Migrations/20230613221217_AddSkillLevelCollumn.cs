using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSquad.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillLevelCollumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "skill_level",
                table: "player",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "skill_level",
                table: "player");
        }
    }
}
