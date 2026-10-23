using BoardGameNight.Domain.Models;
using BoardGameNight.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BoardgameDbContext dbContext;

        public ReviewRepository(BoardgameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public GameNightReview? getById(int id)
        {
            return dbContext.GameNightReviews.Find(id);
        }

        public GameNightReview getReviewsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void add(GameNightReview review)
        {
            dbContext.Add(review);
            dbContext.SaveChanges();
        }

        public void update(GameNightReview review)
        {
            dbContext.Update(review);
            dbContext.SaveChanges();
        }

        public void delete(int id)
        {
            var reviewToRemove = dbContext.GameNightReviews.Find(id);
            if (reviewToRemove != null)
            {
                dbContext.GameNightReviews.Remove(reviewToRemove);
                dbContext.SaveChanges();
            }
        }
    }
}
