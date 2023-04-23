using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.HasKey(transfer => transfer.Id);

        builder.Property(transfer => transfer.Fee)
            .HasPrecision(14, 8);

        builder.Property(transfer => transfer.Salary)
            .HasPrecision(14, 8);
    }
}