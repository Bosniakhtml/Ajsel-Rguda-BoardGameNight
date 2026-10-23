using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardGameNight.Domain.Models;

namespace BoardGameNight.DomainServices
{
    public interface IUserRepository
    {
        public User? getById(int id);

        public IEnumerable<User> getAll();

        public void add(User user);

        public void update(User user);

        public void delete(int id);
    }
}
