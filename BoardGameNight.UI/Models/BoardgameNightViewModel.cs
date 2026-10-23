using BoardGameNight.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BoardGameNight.UI.Models
{
    public class BoardgameNightViewModel
    {
        public bool alreadyEnrolled { get; set; }
        public bool alreadyEnrolledOnDate { get; set; }
        public UserViewModel? Participant { get; set; }
        public int Id { get; init; }
        public string Name { get; set; } = null!;
        public UserViewModel Organizer { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsAdult { get; set; }
        public bool HasNutFreeFood { get; set; }
        public bool HasLactoseFreeFood { get; set; }
        public bool HasVegetarianfood { get; set; }
        public bool HasAlcoholFreeDrinks { get; set; }
        public int MaxParticipants { get; set; }
        public int CurrentNumberOfParticipants { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public string FoodAndDrinks { get; set; } = null!;

        public ICollection<Boardgame> Boardgames { get; set; } = null!;
        public IEnumerable<GameNightReviewViewModel> Reviews { get; set; } = null!;
    }
}
