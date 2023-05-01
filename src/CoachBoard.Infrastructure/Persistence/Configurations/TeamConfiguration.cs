using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(team => team.Id);

        builder.HasMany(team => team.Transfers)
            .WithOne(transfer => transfer.Team)
            .HasForeignKey(transfer => transfer.TeamId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(team => team.Fixtures)
            .WithOne(fixture => fixture.Team)
            .HasForeignKey(fixture => fixture.TeamId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}