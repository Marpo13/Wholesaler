using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations;

public class StorageConfiguration : IEntityTypeConfiguration<Storage>
{
    public void Configure(EntityTypeBuilder<Storage> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(s => s.Name)
            .IsRequired();

        builder
            .HasMany(s => s.Requirements)
            .WithOne(s => s.Storage);
    }
}
