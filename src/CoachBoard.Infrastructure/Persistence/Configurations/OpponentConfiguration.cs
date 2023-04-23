using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class OpponentConfiguration : IEntityTypeConfiguration<Opponent>
{
    public void Configure(EntityTypeBuilder<Opponent> builder)
    {
        builder.HasKey(opponent => opponent.Id);

        builder.HasMany(opponent => opponent.Fixtures)
            .WithOne()
            .HasForeignKey(fixture => fixture.OpponentId);
    }
}