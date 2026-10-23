using BoardGameNight.Domain.Enumerations;
using BoardGameNight.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameNight.UI.Models
{
    public class UserViewModel
    {
        public int Id { get; init; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public Gender Gender { get; set; }

        public bool HasNutAllergy { get; set; }
        public bool IsLactoseIntolerant { get; set; }
        public bool IsVegetarian { get; set; }
        public bool DrinksAlcohol { get; set; }
        public double? AverageScore { get; set; }

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
    }
}
