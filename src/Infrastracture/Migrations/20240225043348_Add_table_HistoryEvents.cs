using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YAGO.FantasyWorld.Server.Infrastracture.Migrations
{
    public partial class Add_table_HistoryEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    HistoryEventEntities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterChanges = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEventEntityWeights",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    HistoryEventId = table.Column<long>(type: "bigint", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEventEntityWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEventEntityWeights_HistoryEvents_HistoryEventId",
                        column: x => x.HistoryEventId,
                        principalTable: "HistoryEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEventEntityWeights_HistoryEventId",
                table: "HistoryEventEntityWeights",
                column: "HistoryEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryEventEntityWeights");

            migrationBuilder.DropTable(
                name: "HistoryEvents");
        }
    }
}
