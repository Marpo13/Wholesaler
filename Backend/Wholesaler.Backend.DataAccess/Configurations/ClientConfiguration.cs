using System.Data.Entity.ModelConfiguration;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            Property(c => c.Name)
                .IsRequired();

            HasMany(c => c.Requirements)
                .WithRequired(r => r.Client);
        }
    }
}