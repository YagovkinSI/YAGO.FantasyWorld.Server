using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YAGO.FantasyWorld.Server.Infrastracture.Migrations
{
    public partial class Add_table_Quests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    QuestEntity1Id = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quests_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quests_OrganizationId",
                table: "Quests",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quests");
        }
    }
}
