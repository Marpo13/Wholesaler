using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations
{
    public class RequirementConfiguration : IEntityTypeConfiguration<Requirement>
    {        
        public void Configure(EntityTypeBuilder<Requirement> builder)
        {
            builder.HasOne(r => r.Client)
                 .WithMany(c => c.Requirements)
                 .HasForeignKey(r => r.ClientId);
        }
    }
}
