using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Infrastructure
{
    public class AuthSeedData(UserManager<IdentityUser> userManager)
    {
        private readonly UserManager<IdentityUser> userManager = userManager;

        public async Task SeedDatabase()
        {
            var users = new[]
            {
                new IdentityUser { UserName = "max@gmail.com", Email = "max@gmail.com" },
                new IdentityUser { UserName = "sarah@gmail.com", Email = "sarah@gmail.com" },
                new IdentityUser { UserName = "lieke@gmail.com", Email = "lieke@gmail.com" },
                new IdentityUser { UserName = "frans@gmail.com", Email = "frans@gmail.com" },
                new IdentityUser { UserName = "paul@gmail.com", Email = "paul@gmail.com" }
            };

            if (!userManager.Users.Any())
            {
                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, "Password@123");
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create user {user.Email}: {string.Join(", ", result.Errors)}");
                    }
                }
            }
        }
    }
}
