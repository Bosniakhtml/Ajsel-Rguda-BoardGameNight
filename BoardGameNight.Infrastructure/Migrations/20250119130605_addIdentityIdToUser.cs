using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addIdentityIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Users");
        }
    }
}
