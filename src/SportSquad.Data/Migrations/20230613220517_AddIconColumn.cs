using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSquad.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIconColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "player_type",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "fa-check");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "icon",
                table: "player_type");
        }
    }
}
