using CoachBoard.Domain.Entities;
using CoachBoard.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoachBoard.Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Career> Careers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder
            .Entity<Player>()
            .Property(player => player.Position)
            .HasConversion(
                v => v.ToString(),
                v => (PlayerPosition)Enum.Parse(typeof(PlayerPosition), v)
            );
    }
}
