using BoardGameNight.Domain.Enumerations;
using BoardGameNight.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Infrastructure
{
    public class BoardgameDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Boardgame> Boardgames { get; set; } = null!;
        public DbSet<BoardgameNight> BoardgameNights { get; set; } = null!;
        public DbSet<GameNightReview> GameNightReviews { get; set; } = null!;

        public BoardgameDbContext(DbContextOptions<BoardgameDbContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardgameNight>().HasMany(x => x.Boardgames).WithMany();
            modelBuilder.Entity<BoardgameNight>().HasMany(x => x.Participants).WithMany().UsingEntity<Dictionary<string, object>>(
                "BoardgameNightParticipant", // Name of the join table
                j => j
                    .HasOne<User>()     // Relationship to Participant
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction), // Prevent cascading delete for Participant
                j => j
                    .HasOne<BoardgameNight>()  // Relationship to BoardgameNight
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)  // Prevent cascading delete for BoardgameNight
            );

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        }
    }
}
