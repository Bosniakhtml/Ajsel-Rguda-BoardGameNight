using BoardGameNight.Domain.Enumerations;
using BoardGameNight.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Infrastructure
{
    public class SeedData
    {
        private BoardgameDbContext dbContext;

        public SeedData(BoardgameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SeedDatabase()
        {
            List<User> users = new()
            {
                new()
                {
                    Email = "max@gmail.com",
                    Name = "Max Verstappen",
                    Street = "Beatrixstraat",
                    City = "Zandvoort",
                    HouseNumber = 20,
                    Gender = Gender.M,
                    BirthDate = new DateTime(2008, 1, 10),
                    DrinksAlcohol = true,
                    HasNutAllergy = false,
                    IsVegetarian = false,
                    IsLactoseIntolerant = false
                },
                new()
                {
                    Email = "sarah@gmail.com",
                    Name = "Sarah Jansen",
                    Street = "Achterstevorenstraat",
                    City = "Dordrecht",
                    HouseNumber = 104,
                    Gender = Gender.F,
                    BirthDate = new DateTime(2000, 8, 20),
                    DrinksAlcohol = false,
                    HasNutAllergy = true,
                    IsVegetarian = false,
                    IsLactoseIntolerant = true
                },
                new()
                {
                    Email = "lieke@gmail.com",
                    Name = "Lieke van der hoef",
                    Street = "Visserijstraat",
                    City = "Goes",
                    HouseNumber = 9,
                    Gender = Gender.F,
                    BirthDate = new DateTime(2008, 1, 10),
                    DrinksAlcohol = true,
                    HasNutAllergy = true,
                    IsVegetarian = true,
                    IsLactoseIntolerant = false
                },
                new()
                {
                    Email = "frans@gmail.com",
                    Name = "Frans Jansen",
                    Street = "Bloemenlaan",
                    City = "Utrecht",
                    HouseNumber = 42,
                    Gender = Gender.M,
                    BirthDate = new DateTime(1995, 5, 15),
                    DrinksAlcohol = false,
                    HasNutAllergy = false,
                    IsVegetarian = false,
                    IsLactoseIntolerant = true
                },
                new()
                {
                    Email = "paul@gmail.com",
                    Name = "Paul de Vries",
                    Street = "Watermolenstraat",
                    City = "Amsterdam",
                    HouseNumber = 7,
                    Gender = Gender.M,
                    BirthDate = new DateTime(1988, 11, 23),
                    DrinksAlcohol = true,
                    HasNutAllergy = true,
                    IsVegetarian = true,
                    IsLactoseIntolerant = false
                }
            };

            List<Boardgame> boardgames = new()
            {
                new()
                {
                    Name = "Catan",
                    Description = "Een strategisch spel waarin spelers grondstoffen verzamelen en nederzettingen bouwen.",
                    IsAdult = false,
                    PhotoURL = "https://media.s-bol.com/J202WVOKQVwv/yqmMWw/550x581.jpg",
                    GameType = BoardGameType.BoardGame,
                    Genre = BoardGameGenre.Strategy
                },
                new()
                {
                    Name = "Cards Against Humanity",
                    Description = "Een partyspel voor mensen met een donkere humor.",
                    IsAdult = true,
                    PhotoURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSizT4NIwju8IMX6QjUQY0qKGfdqJwozPuXag&s",
                    GameType = BoardGameType.CardGame,
                    Genre = BoardGameGenre.Party
                },
                new()
                {
                    Name = "Risk",
                    Description = "Een strategisch oorlogsspel waarin spelers de wereld proberen te veroveren.",
                    IsAdult = false,
                    PhotoURL = "https://media.s-bol.com/vw2V55kxlX70/EvnBq4/550x363.jpg",
                    GameType = BoardGameType.BoardGame,
                    Genre = BoardGameGenre.War
                },
                new()
                {
                    Name = "Pandemic",
                    Description = "Een coöperatief spel waarin spelers samenwerken om wereldwijde uitbraken te stoppen.",
                    IsAdult = false,
                    PhotoURL = "https://www.schaakengo.nl/images/productimages/big/pandemic.jpg",
                    GameType = BoardGameType.BoardGame,
                    Genre = BoardGameGenre.Puzzle
                },
                new()
                {
                    Name = "Ticket to Ride",
                    Description = "Een spel waarin spelers spoorwegverbindingen tussen steden aanleggen.",
                    IsAdult = false,
                    PhotoURL = "https://www.spellenrijk.nl/resize/ZMG91104NL-2_5057509462826.jpg/0/1100/True/pandemic-nl-2.jpg",
                    GameType = BoardGameType.BoardGame,
                    Genre = BoardGameGenre.Strategy
                },
                new()
                {
                    Name = "Exploding Kittens",
                    Description = "Een kaartspel vol strategie en geluk waarin je explosieve kittens moet ontwijken.",
                    IsAdult = false,
                    PhotoURL = "https://www.spellenrijk.nl/resize/ekg-org1-1_1_12513762601036.jpg/0/1100/True/exploding-kittens-origineel.jpg",
                    GameType = BoardGameType.CardGame,
                    Genre = BoardGameGenre.Party
                },
                new()
                {
                    Name = "Monopoly",
                    Description = "Een klassiek spel waarin spelers eigendommen kopen en huur innen om rijkdom te vergaren.",
                    IsAdult = false,
                    PhotoURL = "https://media.s-bol.com/N28qJwkLlpLK/wGQ55r/550x553.jpg",
                    GameType = BoardGameType.BoardGame,
                    Genre = BoardGameGenre.Strategy
                },
                new()
                {
                    Name = "Carcassonne",
                    Description = "Een tegellegspel waarin spelers steden, wegen en kloosters bouwen.",
                    IsAdult = false,
                    PhotoURL = "https://media.s-bol.com/JjQA188RB5Xl/VBYxRM/550x763.jpg",
                    GameType = BoardGameType.BoardGame,
                    Genre = BoardGameGenre.Puzzle
                },
                new()
                {
                    Name = "Uno",
                    Description = "Een klassiek kaartspel waarin spelers proberen zo snel mogelijk hun kaarten kwijt te raken.",
                    IsAdult = false,
                    PhotoURL = "https://media.s-bol.com/m94m5ovy1jNA/1wBBEno/550x819.jpg",
                    GameType = BoardGameType.CardGame,
                    Genre = BoardGameGenre.Party
                },
                new()
                {
                    Name = "7 Wonders",
                    Description = "Een kaartspel waarin spelers beschavingen opbouwen en wereldwonderen bouwen.",
                    IsAdult = false,
                    PhotoURL = "https://www.spellencirkel.nl/wp-content/uploads/2017/02/7-wonders-spel.jpg",
                    GameType = BoardGameType.CardGame,
                    Genre = BoardGameGenre.Strategy
                }
            };

            List<BoardgameNight> boardgameNights = new()
{
            new()
            {
                Organizer = users[1],
                Street = "Tulpenlaan",
                City = "Dordrecht",
                IsAdult = true,
                MaxParticipants = 4,
                DateTime = new DateTime(2025, 1, 10, 19, 0, 0),
                FoodAndDrinks = "Snacks en wijn",
                Name = "Avond vol donkere humor",
                HasAlcoholFreeDrinks = true,
                HasLactoseFreeFood = true,
                HasNutFreeFood = false,
                HasVegetarianfood = true,
                Boardgames = new List<Boardgame> { boardgames[1], boardgames[5] },
                Participants = new List<User> { users[4] },
                Reviews = new List<GameNightReview>
                {
                    new() { Reviewer = users[4], Score = 4, ReviewDescription = "Hilarisch, maar soms te donker voor mijn smaak." }
                }
            },

            new()
            {
                Organizer = users[4],
                Street = "Keizersgracht",
                City = "Amsterdam",
                IsAdult = false,
                MaxParticipants = 5,
                DateTime = new DateTime(2025, 1, 15, 18, 30, 0),
                FoodAndDrinks = "Pizza en frisdrank",
                Name = "Strategieavond",
                HasAlcoholFreeDrinks = true,
                HasLactoseFreeFood = true,
                HasNutFreeFood = true,
                HasVegetarianfood = true,
                Boardgames = new List<Boardgame> { boardgames[0], boardgames[9] },
                Participants = new List<User> { users[0], users[1] },
                Reviews = new List<GameNightReview>
                {
                    new() { Reviewer = users[0], Score = 3, ReviewDescription = "Best intensief met alle strategie, maar interessant." },
                    new() { Reviewer = users[1], Score = 4, ReviewDescription = "Leuke spellen, maar niet voor iedereen." }
                }
            },
            new()
            {
                Organizer = users[3],
                Street = "Maliebaan",
                City = "Utrecht",
                IsAdult = false,
                MaxParticipants = 6,
                DateTime = new DateTime(2025, 1, 18, 17, 0, 0),
                FoodAndDrinks = "Hartige hapjes en thee",
                Name = "Samen puzzelen",
                HasAlcoholFreeDrinks = true,
                HasLactoseFreeFood = true,
                HasNutFreeFood = true,
                HasVegetarianfood = false,
                Boardgames = new List<Boardgame> { boardgames[3], boardgames[7] },
                Participants = new List<User> { users[1], users[2] },
                Reviews = new List<GameNightReview>
                {
                    new() { Reviewer = users[1], Score = 5, ReviewDescription = "Super gezellig en uitdagende spellen!" },
                    new() { Reviewer = users[2], Score = 4, ReviewDescription = "Leuke avond, maar een paar spellen waren moeilijk te volgen." }
                }
            },
            new()
            {
                Organizer = users[4],
                Street = "Watermolenstraat",
                City = "Amsterdam",
                IsAdult = false,
                MaxParticipants = 3,
                DateTime = new DateTime(2025, 4, 10, 20, 0, 0),
                FoodAndDrinks = "Nacho's en limonade",
                Name = "Kaartspelavond",
                HasAlcoholFreeDrinks = true,
                HasLactoseFreeFood = true,
                HasNutFreeFood = false,
                HasVegetarianfood = true,
                Boardgames = new List<Boardgame> { boardgames[8], boardgames[5] },
                Participants = new List<User> { users[2] },
                Reviews = new List<GameNightReview>()
            },
            new()
            {
                Organizer = users[3],
                Street = "Bloemenlaan",
                City = "Utrecht",
                IsAdult = false,
                MaxParticipants = 4,
                DateTime = new DateTime(2025, 5, 5, 19, 0, 0),
                FoodAndDrinks = "Gezonde hapjes en water",
                Name = "Oorlogssimulatieavond",
                HasAlcoholFreeDrinks = true,
                HasLactoseFreeFood = true,
                HasNutFreeFood = true,
                HasVegetarianfood = true,
                Boardgames = new List<Boardgame> { boardgames[2] },
                Participants = new List<User> { users[0], users[2] },
                Reviews = new List<GameNightReview>()
            },
            new()
            {
                Organizer = users[1],
                Street = "Zeestraat",
                City = "Dordrecht",
                IsAdult = false,
                MaxParticipants = 4,
                DateTime = new DateTime(2025, 6, 18, 15, 30, 0),
                FoodAndDrinks = "Zoete snacks en cola",
                Name = "Reis door steden",
                HasAlcoholFreeDrinks = true,
                HasLactoseFreeFood = false,
                HasNutFreeFood = true,
                HasVegetarianfood = true,
                Boardgames = new List<Boardgame> { boardgames[4] },
                Participants = new List<User> { users[0], users[3] },
                Reviews = new List<GameNightReview>()
            },
            new()
            {
                Organizer = users[4],
                Street = "Visserijstraat",
                City = "Goes",
                IsAdult = false,
                MaxParticipants = 6,
                DateTime = new DateTime(2025, 7, 22, 16, 0, 0),
                FoodAndDrinks = "Wraps en jus d'orange",
                Name = "Bouwen en ontwikkelen",
                HasAlcoholFreeDrinks = true,
                HasLactoseFreeFood = true,
                HasNutFreeFood = true,
                HasVegetarianfood = true,
                Boardgames = new List<Boardgame> { boardgames[0], boardgames[6] },
                Participants = new List<User> { users[1], users[3] },
                Reviews = new List<GameNightReview>()
            },
            new()
            {
                Organizer = users[1],
                Street = "Beatrixstraat",
                City = "Zandvoort",
                IsAdult = false,
                MaxParticipants = 4,
                DateTime = new DateTime(2025, 8, 30, 14, 0, 0),
                FoodAndDrinks = "Sandwiches en limonade",
                Name = "Kaartavontuur",
                HasAlcoholFreeDrinks = true,
                HasLactoseFreeFood = true,
                HasNutFreeFood = true,
                HasVegetarianfood = false,
                Boardgames = new List<Boardgame> { boardgames[8], boardgames[9] },
                Participants = new List<User> { users[2] },
                Reviews = new List<GameNightReview>()
            }
        };

            if (dbContext.BoardgameNights?.Count() == 0)
            {
                dbContext.BoardgameNights.AddRange(boardgameNights);
                dbContext.SaveChanges();
            }
        }
    }
}
