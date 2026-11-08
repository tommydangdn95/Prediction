using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Power655.Data.Migrations
{
    /// <inheritdoc />
    public partial class MultiGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Draws",
                table: "Draws");

            migrationBuilder.DropIndex(
                name: "IX_Draws_Date",
                table: "Draws");

            migrationBuilder.AlterColumn<int>(
                name: "Special",
                table: "Draws",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "N6",
                table: "Draws",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Game",
                table: "Draws",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "655"); // dữ liệu trước migration này đều là Power 6/55

            migrationBuilder.AddPrimaryKey(
                name: "PK_Draws",
                table: "Draws",
                columns: new[] { "Game", "DrawId" });

            migrationBuilder.CreateIndex(
                name: "IX_Draws_Game_Date",
                table: "Draws",
                columns: new[] { "Game", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // schema cũ chỉ chứa Power 6/55 (khóa DrawId đơn)
            migrationBuilder.Sql("DELETE FROM Draws WHERE Game <> '655';");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Draws",
                table: "Draws");

            migrationBuilder.DropIndex(
                name: "IX_Draws_Game_Date",
                table: "Draws");

            migrationBuilder.DropColumn(
                name: "Game",
                table: "Draws");

            migrationBuilder.AlterColumn<int>(
                name: "Special",
                table: "Draws",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "N6",
                table: "Draws",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Draws",
                table: "Draws",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_Draws_Date",
                table: "Draws",
                column: "Date");
        }
    }
}
