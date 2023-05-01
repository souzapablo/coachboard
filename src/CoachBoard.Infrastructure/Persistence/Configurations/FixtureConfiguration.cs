using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
{
    public void Configure(EntityTypeBuilder<Fixture> builder)
    {
        builder.HasKey(fixture => fixture.Id);

        builder.HasMany(fixture => fixture.Assists)
            .WithOne(assist => assist.Fixture)
            .HasForeignKey(assist => assist.AssistFixtureId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}