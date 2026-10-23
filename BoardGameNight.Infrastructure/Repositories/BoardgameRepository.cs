using BoardGameNight.Domain.Models;
using BoardGameNight.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Infrastructure.Repositories
{
    public class BoardgameRepository : IBoardgameRepository
    {

        private readonly BoardgameDbContext dbContext;

        public BoardgameRepository(BoardgameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Boardgame? getById(int id)
        {
            return dbContext.Boardgames.Find(id);

        }

        public IEnumerable<Boardgame> getAll()
        {
            return dbContext.Boardgames.ToList();
        }

        public void add(Boardgame boardgame)
        {
            dbContext.Add(boardgame);
            dbContext.SaveChanges();
        }

        public void update(Boardgame boardgame)
        {
            dbContext.Update(boardgame);
            dbContext.SaveChanges();
        }

        public void delete(int id)
        {
            var boardgameToRemove = dbContext.Boardgames.Find(id);
            if (boardgameToRemove != null)
            {
                dbContext.Boardgames.Remove(boardgameToRemove);
                dbContext.SaveChanges();
            }
        }
    }
}
