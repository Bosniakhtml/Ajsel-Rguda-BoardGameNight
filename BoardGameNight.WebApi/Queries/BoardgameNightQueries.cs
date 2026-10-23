using BoardGameNight.Domain.Models;
using BoardGameNight.DomainServices;

namespace BoardGameNight.WebApi.Queries;

public class BoardgameNightQueries()
{
    public ICollection<BoardgameNight> getBoardgameNights([Service] IBoardgameNightService boardgameNightService) => boardgameNightService.getAll();
    public BoardgameNight? getBoardgameNight([Service] IBoardgameNightService boardgameNightService, int id) => boardgameNightService.getById(id)!;
}