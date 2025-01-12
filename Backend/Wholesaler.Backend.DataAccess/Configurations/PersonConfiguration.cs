using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wholesaler.Backend.DataAccess.Converters;
using Wholesaler.Backend.DataAccess.Models;
using Wholesaler.Backend.DataAccess.Models.Helpers;

namespace Wholesaler.Backend.DataAccess.Configurations;

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

        builder
            .HasMany(p => p.Deliveries)
            .WithOne(d => d.Person);

        builder.Property(p => p.RoleInfo)
            .HasConversion(
            new ValueConverter<Role, string>(
                role => JsonSerializer.Serialize(role, role.GetType(), new JsonSerializerOptions { WriteIndented = false }),
                json => JsonSerializer.Deserialize<Role>(json, new JsonSerializerOptions { Converters = { new JsonRoleConverter() } })));
    }
}
