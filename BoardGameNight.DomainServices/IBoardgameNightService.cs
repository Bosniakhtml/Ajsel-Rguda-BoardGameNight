using BoardGameNight.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.DomainServices
{
    public interface IBoardgameNightService
    {
        public BoardgameNight? getById(int id);

        public ICollection<BoardgameNight> getAll();

        public void add(BoardgameNight boardgameNight);

        public void update(BoardgameNight boardgameNight);

        public void delete(int id);

        public void addParticipant(int boardgameNightId, int participantId);

        public void removeParticipant(int boardgameNightId, int participantId);

        public ICollection<BoardgameNight> getAllBoardgameNightsWithUser(int userId);

        public bool userIsEnrolledInGameNightOnDate(int userId, DateTime dateTime);

        public bool userIsEnrolledinBoardgameNight(int userId, int boardgameNightId);
        ICollection<BoardgameNight> getAllBoardgameNightsForUser(int userId);
    }
}
