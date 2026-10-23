using BoardGameNight.Domain.Exceptions;
using BoardGameNight.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.DomainServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IBoardgameNightService boardgameNightService;

        public UserService(IUserRepository repository, IBoardgameNightService boardgameNightService)
        {
            this.repository = repository;
            this.boardgameNightService = boardgameNightService;
        }

        public User? getById(int id)
        {
            return repository.getById(id);
        }

        public IEnumerable<User> getAll()
        {
            return repository.getAll();
        }

        public void add(User user)
        {
            if (!user.isSixteen())
            {
                throw new NotOldEnoughException("Gebruiker moet minstens 16 zijn voordat ze zicht kunnen registeren!");
            }

            repository.add(user);
        }


        public void update(User user)
        {
            repository.update(user);
        }

        public void delete(int id)
        {
            repository.delete(id);
        }

        public User? getByIdentityUserName(IIdentity identity)
        {
            User? user = repository.getAll().SingleOrDefault(u => u.Email == identity.Name);

            return user;
        }

        public double GetReviewScore(User user)
        {
            var boardgameNights = boardgameNightService.getAllBoardgameNightsForUser(user.Id);

            var scores = boardgameNights
                .SelectMany(b => b.Reviews)
                .Select(r => r.Score)
                .ToList();

            if (!scores.Any())
            {
                return 0;
            }

            return Math.Round(scores.Average(), 1);
        }

    }
}
