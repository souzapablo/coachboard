using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(goal => goal.Id);

        builder.HasOne(goal => goal.Assist)
            .WithOne()
            .HasForeignKey<Assist>(assist => assist.GoalId);
    }
}