using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations;

public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.Property(d => d.DeliveryDate)
            .IsRequired();

        builder
            .HasOne(d => d.Person)
            .WithMany(p => p.Deliveries)
            .HasForeignKey(d => d.PersonId);
    }
}
