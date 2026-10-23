using BoardGameNight.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;


namespace BoardGameNight.UI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Street { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public int HouseNumber { get; set; }

        public bool HasNutAllergy { get; set; }
        public bool IsLactoseIntolerant { get; set; }
        public bool IsVegetarian { get; set; }
        public bool DrinksAlcohol { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public required DateTime BirthDate { get; set; }
        public required Gender Gender { get; set; }
        public string? ReturnUrl { get; set; } = "/";
    }
}
