using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class CareerConfiguration : IEntityTypeConfiguration<Career>
{
    public void Configure(EntityTypeBuilder<Career> builder)
    {
        builder.HasKey(career => career.Id);

        builder.HasMany(career => career.Teams)
            .WithOne()
            .HasForeignKey(team => team.CareerId);

        builder.HasMany(career => career.Opponents)
            .WithOne()
            .HasForeignKey(opponent => opponent.CareerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}