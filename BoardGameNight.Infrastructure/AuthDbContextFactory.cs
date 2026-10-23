using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BoardGameNight.Infrastructure;

public sealed class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
{
    public AuthDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("AuthDbConnectionString");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Set the AuthDbConnectionString environment variable before running Entity Framework tools.");
        }

        var options = new DbContextOptionsBuilder<AuthDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new AuthDbContext(options);
    }
}
