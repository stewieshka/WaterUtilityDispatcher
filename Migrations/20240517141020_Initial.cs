using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterUtilityDispatcher.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brigades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brigades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "incidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    address = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    BrigadeId = table.Column<Guid>(type: "uuid", nullable: true),
                    priority = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    status = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_incidents_brigades_BrigadeId",
                        column: x => x.BrigadeId,
                        principalTable: "brigades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "workers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    last_name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    middle_name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    birth_day = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    employment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    salary = table.Column<decimal>(type: "numeric", nullable: false),
                    BrigadeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workers_brigades_BrigadeId",
                        column: x => x.BrigadeId,
                        principalTable: "brigades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "used_materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    IncidentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_used_materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_used_materials_incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "incidents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_incidents_BrigadeId",
                table: "incidents",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_used_materials_IncidentId",
                table: "used_materials",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_workers_BrigadeId",
                table: "workers",
                column: "BrigadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "used_materials");

            migrationBuilder.DropTable(
                name: "workers");

            migrationBuilder.DropTable(
                name: "incidents");

            migrationBuilder.DropTable(
                name: "brigades");
        }
    }
}
