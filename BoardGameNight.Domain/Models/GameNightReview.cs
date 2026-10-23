namespace BoardGameNight.Domain.Models
{
    public class GameNightReview
    {
        public int Id { get; init; }
        public User Reviewer { get; init; } = null!;
        public int Score { get; init; }
        public string ReviewDescription { get; init; } = null!;

    }
}