using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations;

public class WorkdayConfiguration : IEntityTypeConfiguration<Workday>
{
    public void Configure(EntityTypeBuilder<Workday> builder)
    {
        builder
            .HasOne(w => w.Person)
            .WithMany(p => p.Workdays)
            .HasForeignKey(p => p.PersonId);
    }
}
