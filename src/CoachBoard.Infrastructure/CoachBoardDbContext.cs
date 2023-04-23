using System.Reflection;
using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure;

public class CoachBoardDbContext : DbContext
{
    public CoachBoardDbContext(DbContextOptions<CoachBoardDbContext> options)
        : base(options)
    {
    }

    public DbSet<Assist> Assists { get; set; } = null!;
    public DbSet<Career> Careers { get; set; } = null!;
    public DbSet<Fixture> Fixtures { get; set; } = null!;
    public DbSet<Goal> Goals { get; set; } = null!;
    public DbSet<Opponent> Opponents { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Transfer> Transfers { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}