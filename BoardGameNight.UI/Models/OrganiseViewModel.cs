using System.ComponentModel.DataAnnotations;
using BoardGameNight.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoardGameNight.UI.Models
{
    public class OrganiseViewModel
    {
        public UserViewModel? Organizer { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int HouseNumber { get; set; }

        [Required]
        public string Street { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public bool IsAdult { get; set; }

        [Required]
        public int MaxParticipants { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTime { get; set; }

        [Required]
        public bool HasNutFreeFood { get; set; }

        [Required]
        public bool HasLactoseFreeFood { get; set; }

        [Required]
        public bool HasVegetarianfood { get; set; }

        [Required]
        public bool HasAlcoholFreeDrinks { get; set; }

        [Required]
        public string FoodAndDrinks { get; set; } = null!;

        [Required]
        public List<int> SelectedBoardgameIds { get; set; } = new List<int>();

        public SelectList? AvailableBoardgames { get; set; }

        public string? ReturnUrl { get; set; } = "/BoardgameNight/Participate";
    }
}
