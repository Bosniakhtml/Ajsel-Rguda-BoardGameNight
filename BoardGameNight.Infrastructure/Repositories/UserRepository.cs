using BoardGameNight.Domain.Models;
using BoardGameNight.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BoardgameDbContext dbContext;

        public UserRepository(BoardgameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User? getById(int id)
        {
            return dbContext.Users.Find(id);

        }

        public IEnumerable<User> getAll()
        {
            return dbContext.Users;
        }

        public void add(User user)
        {
            dbContext.Add(user);
            dbContext.SaveChanges();
        }

        public void update(User user)
        {
            dbContext.Update(user);
            dbContext.SaveChanges();
        }

        public void delete(int id)
        {
            var userToRemove = dbContext.Users.Find(id);
            if (userToRemove != null)
            {
                dbContext.Users.Remove(userToRemove);
                dbContext.SaveChanges();
            }
        }
    }
}
