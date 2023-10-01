using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSquad.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedForPlayersType : Migration
    {
        private Guid _playerRowId = Guid.Parse("38629742-7460-40B5-A68E-CC5A6AB3AA99");
        private Guid _playerGoalkeeperId = Guid.Parse("05C06FF8-C180-4A08-8AF1-A6B0DAF8472A");

        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "player_type",
                columnTypes: new[] {"guid", "datetime", "integer", "varchar", "varchar"},
                columns: new[] { "id", "created_at", "code", "name", "icon" },
                values: new object[,]
                {
                    { _playerRowId, DateTime.UtcNow, 1, "Linha", "shoe-prints" },
                    { _playerGoalkeeperId, DateTime.UtcNow, 2, "Goleiro", "sign-language" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var keys = new Object[] { _playerRowId, _playerGoalkeeperId };
            
            migrationBuilder.DeleteData(
                table: "player_type",
                keyColumn: "id",
                keyColumnType: "Guid",
                keyValues: keys
            );
        }
    }
}
