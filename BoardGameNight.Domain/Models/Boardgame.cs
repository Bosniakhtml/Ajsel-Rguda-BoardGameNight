namespace BoardGameNight.Domain.Models;

using BoardGameNight.Domain.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

public class Boardgame
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAdult { get; set; }
    public string PhotoURL { get; set; } = null!;

    [Column(TypeName = "nvarchar(MAX)")]
    public BoardGameType GameType { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public BoardGameGenre Genre { get; set; }
}
