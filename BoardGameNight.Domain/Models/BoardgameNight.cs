using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Domain.Models
{
    public class BoardgameNight
    {
        public int Id { get; init; }
        public string Name { get; set; } = null!;
        public User Organizer { get; set; } = null!;
        public int HouseNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public bool IsAdult { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime DateTime { get; set; }
        public bool HasNutFreeFood { get; set; }
        public bool HasLactoseFreeFood { get; set; }
        public bool HasVegetarianfood { get; set; }
        public bool HasAlcoholFreeDrinks { get; set; }
        public string FoodAndDrinks { get; set; } = null!;

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public ICollection<User> Participants { get; set; } = null!;
        public ICollection<Boardgame> Boardgames { get; set; } = null!;
        public ICollection<GameNightReview> Reviews { get; set; } = null!;

        public string getAddress()
        {
            return $"{City}, {Street} {HouseNumber}";
        }

        public bool hasRoomForParticipant()
        {
            if (Participants.Count != MaxParticipants)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
