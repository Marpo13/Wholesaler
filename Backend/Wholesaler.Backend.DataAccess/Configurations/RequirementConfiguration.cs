using System.Data.Entity.ModelConfiguration;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations
{
    public class RequirementConfiguration : EntityTypeConfiguration<Requirement>
    {
        public RequirementConfiguration()
        {
            HasRequired(r => r.Client)
                .WithMany(c => c.Requirements)
                .HasForeignKey(r => r.ClientId);

            HasMany(r => r.Tasks)
                .WithRequired(t => t.Requirement);
        }
    }
}
