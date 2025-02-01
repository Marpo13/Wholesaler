using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations;

internal class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder
            .HasOne(a => a.WorkTask)
            .WithMany(w => w.Activities)
            .HasForeignKey(a => a.WorkTaskId);

        builder
            .HasOne(a => a.Person)
            .WithMany(p => p.Activities)
            .HasForeignKey(a => a.PersonId);
    }
}
