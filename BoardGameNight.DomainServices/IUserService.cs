using BoardGameNight.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.DomainServices
{
    public interface IUserService
    {
        public User? getById(int id);

        public User? getByIdentityUserName(IIdentity identity);

        public IEnumerable<User> getAll();

        public void add(User user);

        public void update(User user);

        public void delete(int id);

        public double GetReviewScore(User user);
    }
}
