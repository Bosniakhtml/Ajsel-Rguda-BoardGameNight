using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndBoardgameNightFoodPreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "drinksAlcohol",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "hasNutAllergy",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isLactoseIntolerant",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVegetarian",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "hasAlcoholFreeDrinks",
                table: "BoardgameNights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "hasLactoseFreeFood",
                table: "BoardgameNights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "hasNutFreeFood",
                table: "BoardgameNights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "hasVegetarianfood",
                table: "BoardgameNights",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "drinksAlcohol",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "hasNutAllergy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isLactoseIntolerant",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isVegetarian",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "hasAlcoholFreeDrinks",
                table: "BoardgameNights");

            migrationBuilder.DropColumn(
                name: "hasLactoseFreeFood",
                table: "BoardgameNights");

            migrationBuilder.DropColumn(
                name: "hasNutFreeFood",
                table: "BoardgameNights");

            migrationBuilder.DropColumn(
                name: "hasVegetarianfood",
                table: "BoardgameNights");
        }
    }
}
