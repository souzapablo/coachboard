using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class AssistConfiguration : IEntityTypeConfiguration<Assist>
{
    public void Configure(EntityTypeBuilder<Assist> builder)
    {
        builder.HasKey(assist => assist.Id);
    }
}