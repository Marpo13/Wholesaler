using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations
{
    public class WorkTaskConfiguration : IEntityTypeConfiguration<WorkTask>
    {
        public void Configure(EntityTypeBuilder<WorkTask> builder)
        {
            builder
                .HasOne(w => w.Person)
                .WithMany(p => p.WorkTasks)
                .HasForeignKey(p => p.PersonId);
        }
    }
}
