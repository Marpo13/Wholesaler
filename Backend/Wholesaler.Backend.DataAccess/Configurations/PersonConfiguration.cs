using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(p => p.Name)
               .IsRequired();

            builder.Property(p => p.Surname)
                .IsRequired();

            builder.Property(p => p.Login)
                .IsRequired();

            builder.Property(p => p.Password)
                .IsRequired();

            builder.Property(p => p.Role)
                .IsRequired();

            builder
                .HasMany(p => p.Workdays)
                .WithOne(w => w.Person);

            builder
                .HasMany(p => p.WorkTasks)
                .WithOne(w => w.Person);

            builder
                .HasMany(p => p.Activities)
                .WithOne(a => a.Person);
        }
    }
}
