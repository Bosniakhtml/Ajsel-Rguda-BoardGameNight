using BoardGameNight.Domain.Models;
using BoardGameNight.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Infrastructure.Repositories
{
    public class BoardgameNightRepository : IBoardgameNightRepository
    {

        private readonly BoardgameDbContext dbContext;

        public BoardgameNightRepository(BoardgameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public BoardgameNight? getById(int id)
        {
            return dbContext.BoardgameNights.Include(boardgameNight => boardgameNight.Organizer)
                .Include(boardgameNight => boardgameNight.Participants)
                .Include(boardgameNight => boardgameNight.Reviews)
                    .ThenInclude(review => review.Reviewer)
                .Include(boardgameNight => boardgameNight.Boardgames)
                .FirstOrDefault(x => x.Id == id);
        }

        public ICollection<BoardgameNight> getAll()
        {
            return dbContext.BoardgameNights.Include(boardgameNight => boardgameNight.Organizer)
                .Include(boardgameNight => boardgameNight.Participants)
                .Include(boardgameNight => boardgameNight.Reviews)
                    .ThenInclude(review => review.Reviewer)
                .Include(boardgameNight => boardgameNight.Boardgames).ToList();
        }

        public void add(BoardgameNight boardgameNight)
        {
            dbContext.Add(boardgameNight);
            dbContext.SaveChanges();
        }

        public void update(BoardgameNight boardgameNight)
        {
            dbContext.Update(boardgameNight);
            dbContext.SaveChanges();
        }

        public void delete(int id)
        {
            var boardgameNightToRemove = dbContext.BoardgameNights.Find(id);
            if (boardgameNightToRemove != null)
            {
                dbContext.BoardgameNights.Remove(boardgameNightToRemove);
                dbContext.SaveChanges();
            }
        }
    }
}
