using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BoardGameNight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Boardgames",
                columns: new[] { "Id", "Description", "GameType", "Genre", "IsAdult", "Name", "PhotoURL" },
                values: new object[,]
                {
                    { 1, "Een strategisch spel waarin spelers grondstoffen verzamelen en nederzettingen bouwen.", "BoardGame", "Strategy", false, "Catan", "https://media.s-bol.com/J202WVOKQVwv/yqmMWw/550x581.jpg" },
                    { 2, "Een partyspel voor mensen met een donkere humor.", "CardGame", "Party", true, "Cards Against Humanity", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSizT4NIwju8IMX6QjUQY0qKGfdqJwozPuXag&s" },
                    { 3, "Een strategisch oorlogsspel waarin spelers de wereld proberen te veroveren.", "BoardGame", "War", false, "Risk", "https://media.s-bol.com/vw2V55kxlX70/EvnBq4/550x363.jpg" },
                    { 4, "Een coöperatief spel waarin spelers samenwerken om wereldwijde uitbraken te stoppen.", "BoardGame", "Puzzle", false, "Pandemic", "https://example.com/pandemic.jpg" },
                    { 5, "Een spel waarin spelers spoorwegverbindingen tussen steden aanleggen.", "BoardGame", "Strategy", false, "Ticket to Ride", "https://www.spellenrijk.nl/resize/ZMG91104NL-2_5057509462826.jpg/0/1100/True/pandemic-nl-2.jpg" },
                    { 6, "Een kaartspel vol strategie en geluk waarin je explosieve kittens moet ontwijken.", "CardGame", "Party", false, "Exploding Kittens", "https://www.spellenrijk.nl/resize/ekg-org1-1_1_12513762601036.jpg/0/1100/True/exploding-kittens-origineel.jpg" },
                    { 7, "Een klassiek spel waarin spelers eigendommen kopen en huur innen om rijkdom te vergaren.", "BoardGame", "Strategy", false, "Monopoly", "https://media.s-bol.com/N28qJwkLlpLK/wGQ55r/550x553.jpg" },
                    { 8, "Een tegellegspel waarin spelers steden, wegen en kloosters bouwen.", "BoardGame", "Puzzle", false, "Carcassonne", "https://media.s-bol.com/JjQA188RB5Xl/VBYxRM/550x763.jpg" },
                    { 9, "Een klassiek kaartspel waarin spelers proberen zo snel mogelijk hun kaarten kwijt te raken.", "CardGame", "Party", false, "Uno", "https://media.s-bol.com/m94m5ovy1jNA/1wBBEno/550x819.jpg" },
                    { 10, "Een kaartspel waarin spelers beschavingen opbouwen en wereldwonderen bouwen.", "CardGame", "Strategy", false, "7 Wonders", "https://www.spellencirkel.nl/wp-content/uploads/2017/02/7-wonders-spel.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Boardgames",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
