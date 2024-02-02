using CoachBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Configurations;
public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(user => user.Email)
            .IsUnique();

        builder.HasIndex(user => user.Username)
            .IsUnique();

        builder.HasQueryFilter(user => !user.IsDeleted);
    }
}
