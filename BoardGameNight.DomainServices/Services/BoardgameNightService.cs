using BoardGameNight.Domain.Exceptions;
using BoardGameNight.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.DomainServices.Services
{
    public class BoardgameNightService : IBoardgameNightService
    {
        private readonly IBoardgameNightRepository repository;
        private readonly IUserRepository userRepository;

        public BoardgameNightService(IBoardgameNightRepository repository, IUserRepository userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
        }

        public BoardgameNight? getById(int id)
        {
            return repository.getById(id);
        }

        public ICollection<BoardgameNight> getAll()
        {
            return repository.getAll();
        }

        public void add(BoardgameNight boardgameNight)
        {
            if (boardgameNight.Boardgames.Any(bg => bg.IsAdult))
            {
                boardgameNight.IsAdult = true;
            }
            else
            {
                boardgameNight.IsAdult = false;
            }

            if (!boardgameNight.Organizer.isEighteen())
            {
                throw new NotOldEnoughException("Gebruiker moet minstens 18 zijn voordat ze bordspelavonden kunnen organiseren!");
            }

            repository.add(boardgameNight);
        }

        public void update(BoardgameNight boardgameNight)
        {
            if (boardgameNight.Boardgames.Any(bg => bg.IsAdult))
            {
                boardgameNight.IsAdult = true;
            }
            else
            {
                boardgameNight.IsAdult = false;
            }
            if (!boardgameNight.Participants.IsNullOrEmpty())
            {
                throw new Exception("Kan een bordspellen avond met active deelenemers niet wijzigen");
            }
            repository.update(boardgameNight);
        }

        public void delete(int id)
        {
            BoardgameNight? boardgameNight = repository.getById(id) ?? throw new InvalidDataException("Bordspellen avond bestaat niet");

            if (!boardgameNight.Participants.IsNullOrEmpty())
            {
                throw new Exception("Kan een bordspellen avond met active deelenemers niet verwijderen");
            }
            repository.delete(id);
        }

        public void addParticipant(int boardgameNightId, int participantId)
        {

            BoardgameNight? boardgameNight = getById(boardgameNightId);
            User? participant = userRepository.getById(participantId);

            if (boardgameNight == null) throw new InvalidDataException("Bordspellen avond bestaat niet");

            if (participant == null) throw new InvalidDataException("Gebruiker bestaat niet");

            if (userIsEnrolledinBoardgameNight(participantId, boardgameNightId)) throw new AlreadyEnrolledInBoardgameNight("Kan niet opnieuw inschrijven voor dezelfde bordspellen avond");

            if (!boardgameNight.hasRoomForParticipant()) throw new GameNightIsFullException("Bordspellen avond zit vol");

            if (boardgameNight.Organizer.Id == participantId) throw new OrganizerEnrolledInOwnBoardgameNight("Organisator kan niet inschrijven in eigen bordspellen avond");

            if (boardgameNight.IsAdult)
            {
                if(!participant.isEighteen()) throw new NotOldEnoughException("Gebruiker is niet oud genoeg om deel te nemen aan deze bord spellen avond");
            }

            if (userIsEnrolledInGameNightOnDate(participantId, boardgameNight.DateTime)) throw new AlreadyEnrolledInBoardgameNightDate();

            boardgameNight.Participants.Add(participant);
            repository.update(boardgameNight);
        }

        public void removeParticipant(int boardgameNightId, int participantId)
        {

            BoardgameNight? boardgameNight = getById(boardgameNightId);
            User? participant = userRepository.getById(participantId);

            if (boardgameNight == null) throw new InvalidDataException("Bordspellen avond bestaat niet");

            if (participant == null) throw new InvalidDataException("Gebruiker bestaat niet");

            boardgameNight.Participants.Remove(participant);
            repository.update(boardgameNight);
        }

        public ICollection<BoardgameNight> getAllBoardgameNightsWithUser(int userId)
        {

            if (userRepository.getById(userId) == null) throw new InvalidDataException("Gebruiker Bestaat niet");

            ICollection<BoardgameNight> boardgameNights = repository.getAll();

            ICollection<BoardgameNight> result = boardgameNights
            .Where(bn => bn.Participants.Any(p => p.Id == userId))
            .ToList();

            return result;
        }

        public bool userIsEnrolledInGameNightOnDate(int userId, DateTime dateTime)
        {
            if (userRepository.getById(userId) == null) throw new InvalidDataException("Gebruiker bestaat niet");

            ICollection<BoardgameNight> enrolledBoardgames = getAllBoardgameNightsWithUser(userId);

            bool isAlreadyEnrolled = enrolledBoardgames.Any(bn => bn.DateTime.Date == dateTime.Date);

            return isAlreadyEnrolled;
        }

        public bool userIsEnrolledinBoardgameNight(int userId, int boardgameNightId)
        {
            BoardgameNight? boardgameNight = repository.getById(boardgameNightId);

            if (userRepository.getById(userId) == null) throw new InvalidDataException("Gebruiker bestaat niet");
            if (boardgameNight == null) throw new InvalidDataException("Bordspellen avond bestaat niet");

            ICollection<BoardgameNight> enrolledBoardgames = getAllBoardgameNightsWithUser(userId);

            bool isAlreadyEnrolled = enrolledBoardgames.Contains(boardgameNight);

            return isAlreadyEnrolled;
        }

        public ICollection<BoardgameNight> getAllBoardgameNightsForUser(int userId)
        {

            if (userRepository.getById(userId) == null) throw new InvalidDataException("Gebruiker Bestaat niet");

            ICollection<BoardgameNight> boardgameNights = repository.getAll();

            ICollection<BoardgameNight> result = boardgameNights
            .Where(bn => bn.Organizer.Id == userId)
            .ToList();

            return result;
        }
    }
}
