using System.Data.Entity.ModelConfiguration;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations
{
    public class WorkTaskConfiguration : EntityTypeConfiguration<WorkTask>
    {
        public WorkTaskConfiguration()
        {
            HasRequired(t => t.Requirement)
                .WithMany(r => r.Tasks)
                .HasForeignKey(t => t.ReguirementId);

            HasRequired(t => t.Person)
                .WithRequiredPrincipal(p => p.Task);
        }
    }
}
