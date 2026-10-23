using BoardGameNight.Domain.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameNight.Domain.Models

{
    public class User
    {

        public int Id { get; init; }
        public string? IdentityId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public int HouseNumber { get; set; }
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public Gender Gender { get; set; }

        public bool HasNutAllergy { get; set; }
        public bool IsLactoseIntolerant { get; set; }
        public bool IsVegetarian { get; set; }
        public bool DrinksAlcohol { get; set; }

        public string getAddress()
        {
            return $"{City}, {Street} {HouseNumber}";
        }

        public bool isEighteen()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - BirthDate.Year;

            if (BirthDate > today.AddYears(-age))
            {
                age--;
            }

            return age >= 18;
        }

        public bool isSixteen()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - BirthDate.Year;

            if (BirthDate > today.AddYears(-age))
            {
                age--;
            }

            return age >= 16;
        }
    }
}
