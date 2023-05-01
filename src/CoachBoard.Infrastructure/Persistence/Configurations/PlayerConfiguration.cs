using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(player => player.Id);

        builder.HasMany(player => player.Goals)
            .WithOne(goal => goal.PlayerScored)
            .HasForeignKey(goal => goal.PlayerScoredId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        builder.HasMany(player => player.Assists)
            .WithOne(assist => assist.PlayerAssisted)
            .HasForeignKey(assist => assist.PlayerAssistedId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(fixture => fixture.Fixtures)
            .WithMany(player => player.LineUp)
            .UsingEntity<Dictionary<string, object>>(
                "FixturePlayer",
                j => j.HasOne<Fixture>().WithMany().OnDelete(DeleteBehavior.ClientSetNull),
                j => j.HasOne<Player>().WithMany().OnDelete(DeleteBehavior.ClientSetNull));
    }
}