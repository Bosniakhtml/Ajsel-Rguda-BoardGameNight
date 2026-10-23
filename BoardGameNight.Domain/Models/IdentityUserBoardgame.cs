namespace BoardGameNight.Domain.Models
{
    public class IdentityUserBoardgame
    {
        public int Id { get; set; }
        public string? IdentityId { get; set; }
        public required string Name { get; set; }
    }
}
