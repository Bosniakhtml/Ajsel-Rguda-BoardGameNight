using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initalSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boardgames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdult = table.Column<bool>(type: "bit", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameType = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boardgames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardgameNights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<int>(type: "int", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdult = table.Column<bool>(type: "bit", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FoodAndDrinks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardgameNights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardgameNights_Users_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardgameBoardgameNight",
                columns: table => new
                {
                    BoardgameNightId = table.Column<int>(type: "int", nullable: false),
                    BoardgamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardgameBoardgameNight", x => new { x.BoardgameNightId, x.BoardgamesId });
                    table.ForeignKey(
                        name: "FK_BoardgameBoardgameNight_BoardgameNights_BoardgameNightId",
                        column: x => x.BoardgameNightId,
                        principalTable: "BoardgameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardgameBoardgameNight_Boardgames_BoardgamesId",
                        column: x => x.BoardgamesId,
                        principalTable: "Boardgames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardgameNightParticipant",
                columns: table => new
                {
                    BoardgameNightId = table.Column<int>(type: "int", nullable: false),
                    ParticipantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardgameNightParticipant", x => new { x.BoardgameNightId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_BoardgameNightParticipant_BoardgameNights_BoardgameNightId",
                        column: x => x.BoardgameNightId,
                        principalTable: "BoardgameNights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardgameNightParticipant_Users_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameNightReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ReviewDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardgameNightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNightReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameNightReviews_BoardgameNights_BoardgameNightId",
                        column: x => x.BoardgameNightId,
                        principalTable: "BoardgameNights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameNightReviews_Users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardgameBoardgameNight_BoardgamesId",
                table: "BoardgameBoardgameNight",
                column: "BoardgamesId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardgameNightParticipant_ParticipantsId",
                table: "BoardgameNightParticipant",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardgameNights_OrganizerId",
                table: "BoardgameNights",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNightReviews_BoardgameNightId",
                table: "GameNightReviews",
                column: "BoardgameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNightReviews_ReviewerId",
                table: "GameNightReviews",
                column: "ReviewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardgameBoardgameNight");

            migrationBuilder.DropTable(
                name: "BoardgameNightParticipant");

            migrationBuilder.DropTable(
                name: "GameNightReviews");

            migrationBuilder.DropTable(
                name: "Boardgames");

            migrationBuilder.DropTable(
                name: "BoardgameNights");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
