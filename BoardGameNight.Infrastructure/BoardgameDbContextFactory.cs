using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BoardGameNight.Infrastructure;

public sealed class BoardgameDbContextFactory : IDesignTimeDbContextFactory<BoardgameDbContext>
{
    public BoardgameDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("BoardgameConnectionString");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Set the BoardgameConnectionString environment variable before running Entity Framework tools.");
        }

        var options = new DbContextOptionsBuilder<BoardgameDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new BoardgameDbContext(options);
    }
}
