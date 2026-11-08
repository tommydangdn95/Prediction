using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Power655.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Draws",
                columns: table => new
                {
                    DrawId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    N1 = table.Column<int>(type: "INTEGER", nullable: false),
                    N2 = table.Column<int>(type: "INTEGER", nullable: false),
                    N3 = table.Column<int>(type: "INTEGER", nullable: false),
                    N4 = table.Column<int>(type: "INTEGER", nullable: false),
                    N5 = table.Column<int>(type: "INTEGER", nullable: false),
                    N6 = table.Column<int>(type: "INTEGER", nullable: false),
                    Special = table.Column<int>(type: "INTEGER", nullable: false),
                    Jackpot1 = table.Column<long>(type: "INTEGER", nullable: true),
                    Jackpot2 = table.Column<long>(type: "INTEGER", nullable: true),
                    Source = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    FetchedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.DrawId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Draws_Date",
                table: "Draws",
                column: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Draws");
        }
    }
}
