using BoardGameNight.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.DomainServices.Services
{
    public class BoardgameService : IBoardgameService
    {
        private readonly IBoardgameRepository repository;

        public BoardgameService(IBoardgameRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Boardgame> getAll()
        {
            return repository.getAll();
        }

        public Boardgame? getById(int id)
        {
            return repository.getById(id);
        }

        public void add(Boardgame boardGame)
        {
            repository.add(boardGame);
        }

        public void update(Boardgame boardGame)
        {
            repository.update(boardGame);
        }

        public void delete(int id)
        {
            repository.delete(id);
        }
    }
}
