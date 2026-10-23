using BoardGameNight.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.DomainServices.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository repository;

        public ReviewService(IReviewRepository repository)
        {
            this.repository = repository;
        }

        public GameNightReview? getById(int id)
        {
            return repository.getById(id);
        }

        public GameNightReview getReviewsByUser(int userId)
        {
            return repository.getReviewsByUser(userId);
        }

        public void add(GameNightReview review)
        {
            repository.add(review);
        }

        public void update(GameNightReview review)
        {
            repository.update(review);
        }

        public void delete(int id)
        {
            repository.delete(id);
        }
    }
}
