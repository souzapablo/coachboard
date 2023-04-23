using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachBoard.Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(player => player.Id);

        builder.HasMany(player => player.Transfers)
            .WithOne()
            .HasForeignKey(transfer => transfer.PlayerTransferredId)
            .OnDelete(DeleteBehavior.NoAction);
        ;

        builder.HasMany(player => player.Assists)
            .WithOne()
            .HasForeignKey(assist => assist.PlayerAssistedId)
            .OnDelete(DeleteBehavior.NoAction);
        ;
    }
}