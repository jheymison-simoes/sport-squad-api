using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportSquad.Data.Migrations
{
    public partial class InitialCreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "PlayerSequence");

            migrationBuilder.CreateSequence(
                name: "PlayerTypeSequence");

            migrationBuilder.CreateSequence(
                name: "SquadConfigSequence");

            migrationBuilder.CreateSequence(
                name: "SquadSequence");

            migrationBuilder.CreateSequence(
                name: "UserSequence");

            migrationBuilder.CreateTable(
                name: "player_type",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"PlayerTypeSequence\"')"),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_player_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"UserSequence\"')"),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    ddd = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    email = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    password = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "squad",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"SquadSequence\"')"),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_squad", x => x.id);
                    table.ForeignKey(
                        name: "fk_squad_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"PlayerSequence\"')"),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    player_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    squad_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_player", x => x.id);
                    table.ForeignKey(
                        name: "fk_player_player_type_id",
                        column: x => x.player_type_id,
                        principalTable: "player_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_player_squad_id",
                        column: x => x.squad_id,
                        principalTable: "squad",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_player_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "squad_config",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"SquadConfigSequence\"')"),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity_players = table.Column<int>(type: "integer", nullable: false),
                    squad_id = table.Column<Guid>(type: "uuid", nullable: false),
                    player_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_squad_config", x => x.id);
                    table.ForeignKey(
                        name: "fk_squad_config_player_type_id",
                        column: x => x.player_type_id,
                        principalTable: "player_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_squad_config_user_id",
                        column: x => x.squad_id,
                        principalTable: "squad",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_player_player_type_id",
                table: "player",
                column: "player_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_player_squad_id",
                table: "player",
                column: "squad_id");

            migrationBuilder.CreateIndex(
                name: "ix_player_user_id",
                table: "player",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_squad_user_id",
                table: "squad",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_squad_config_player_type_id",
                table: "squad_config",
                column: "player_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_squad_config_squad_id",
                table: "squad_config",
                column: "squad_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player");

            migrationBuilder.DropTable(
                name: "squad_config");

            migrationBuilder.DropTable(
                name: "player_type");

            migrationBuilder.DropTable(
                name: "squad");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropSequence(
                name: "PlayerSequence");

            migrationBuilder.DropSequence(
                name: "PlayerTypeSequence");

            migrationBuilder.DropSequence(
                name: "SquadConfigSequence");

            migrationBuilder.DropSequence(
                name: "SquadSequence");

            migrationBuilder.DropSequence(
                name: "UserSequence");
        }
    }
}
