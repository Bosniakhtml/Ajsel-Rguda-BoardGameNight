using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class quickfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isVegetarian",
                table: "Users",
                newName: "IsVegetarian");

            migrationBuilder.RenameColumn(
                name: "isLactoseIntolerant",
                table: "Users",
                newName: "IsLactoseIntolerant");

            migrationBuilder.RenameColumn(
                name: "hasNutAllergy",
                table: "Users",
                newName: "HasNutAllergy");

            migrationBuilder.RenameColumn(
                name: "drinksAlcohol",
                table: "Users",
                newName: "DrinksAlcohol");

            migrationBuilder.RenameColumn(
                name: "hasVegetarianfood",
                table: "BoardgameNights",
                newName: "HasVegetarianfood");

            migrationBuilder.RenameColumn(
                name: "hasNutFreeFood",
                table: "BoardgameNights",
                newName: "HasNutFreeFood");

            migrationBuilder.RenameColumn(
                name: "hasLactoseFreeFood",
                table: "BoardgameNights",
                newName: "HasLactoseFreeFood");

            migrationBuilder.RenameColumn(
                name: "hasAlcoholFreeDrinks",
                table: "BoardgameNights",
                newName: "HasAlcoholFreeDrinks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVegetarian",
                table: "Users",
                newName: "isVegetarian");

            migrationBuilder.RenameColumn(
                name: "IsLactoseIntolerant",
                table: "Users",
                newName: "isLactoseIntolerant");

            migrationBuilder.RenameColumn(
                name: "HasNutAllergy",
                table: "Users",
                newName: "hasNutAllergy");

            migrationBuilder.RenameColumn(
                name: "DrinksAlcohol",
                table: "Users",
                newName: "drinksAlcohol");

            migrationBuilder.RenameColumn(
                name: "HasVegetarianfood",
                table: "BoardgameNights",
                newName: "hasVegetarianfood");

            migrationBuilder.RenameColumn(
                name: "HasNutFreeFood",
                table: "BoardgameNights",
                newName: "hasNutFreeFood");

            migrationBuilder.RenameColumn(
                name: "HasLactoseFreeFood",
                table: "BoardgameNights",
                newName: "hasLactoseFreeFood");

            migrationBuilder.RenameColumn(
                name: "HasAlcoholFreeDrinks",
                table: "BoardgameNights",
                newName: "hasAlcoholFreeDrinks");
        }
    }
}
