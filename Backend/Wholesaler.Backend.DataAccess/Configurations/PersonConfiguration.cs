using System.Data.Entity.ModelConfiguration;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Property(p => p.Name)
                .IsRequired();

            Property(p => p.Surname)
                .IsRequired();

            Property(p => p.Login)
                .IsRequired();

            Property(p => p.Password)
                .IsRequired();

            Property(p => p.Role)
                .IsRequired();

            HasRequired(p => p.Task)
                .WithRequiredDependent(t => t.Person);
        }
    }
}
