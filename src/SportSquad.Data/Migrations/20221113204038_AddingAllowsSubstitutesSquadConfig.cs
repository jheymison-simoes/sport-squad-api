using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSquad.Data.Migrations
{
    public partial class AddingAllowsSubstitutesSquadConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "allow_substitutes",
                table: "squad_config",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "allow_substitutes",
                table: "squad_config");
        }
    }
}
