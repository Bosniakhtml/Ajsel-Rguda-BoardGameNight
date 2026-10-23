using BoardGameNight.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.DomainServices
{
    public interface IBoardgameNightRepository
    {
        public BoardgameNight? getById(int id);

        public ICollection<BoardgameNight> getAll();

        public void add(BoardgameNight boardgameNight);

        public void update(BoardgameNight boardgameNight);

        public void delete(int id);
    }
}
