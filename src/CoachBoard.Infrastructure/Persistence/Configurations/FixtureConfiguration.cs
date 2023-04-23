using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
{
    public void Configure(EntityTypeBuilder<Fixture> builder)
    {
        builder.HasKey(fixture => fixture.Id);

        builder.HasMany(fixture => fixture.LineUp)
            .WithMany(player => player.Fixtures)
            .UsingEntity<Dictionary<string, object>>(
                "FixturePlayer",
                j => j.HasOne<Player>().WithMany().OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Fixture>().WithMany().OnDelete(DeleteBehavior.Restrict));
        
        builder.HasMany(fixture => fixture.Goals)
            .WithOne()
            .HasForeignKey(goal => goal.FixtureId);
    }
}