using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(team => team.Id);

        builder.HasMany(team => team.Squad)
            .WithOne()
            .HasForeignKey(player => player.TeamId);

        builder.HasMany(team => team.Fixtures)
            .WithOne()
            .HasForeignKey(fixture => fixture.TeamId);

        builder.HasMany(team => team.Transfers)
            .WithOne()
            .HasForeignKey(transfer => transfer.TeamId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}