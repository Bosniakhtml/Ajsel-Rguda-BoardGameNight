using BoardGameNight.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BoardGameNight.UI.Models
{
    public class GameNightReviewViewModel
    {
        public int Id { get; init; }
        public UserViewModel Reviewer { get; init; } = null!;
        public int Score { get; init; }
        public string ReviewDescription { get; init; } = null!;
        public int BoardgameNightId { get; init; }
    }
}
