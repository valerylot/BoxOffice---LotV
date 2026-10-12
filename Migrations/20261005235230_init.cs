using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoxOffice___LotV.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Artist = table.Column<string>(type: "TEXT", nullable: false),
                    OpeningAct = table.Column<string>(type: "TEXT", nullable: true),
                    Venue = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    TicketPrice = table.Column<double>(type: "REAL", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    TicketsSold = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concerts");
        }
    }
}
