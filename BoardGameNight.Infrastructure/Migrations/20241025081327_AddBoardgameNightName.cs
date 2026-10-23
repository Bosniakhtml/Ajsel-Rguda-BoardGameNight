using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBoardgameNightName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BoardgameNights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "BoardgameNights");
        }
    }
}
