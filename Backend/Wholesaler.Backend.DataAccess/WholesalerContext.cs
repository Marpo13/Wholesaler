using Microsoft.EntityFrameworkCore;
using Wholesaler.Backend.DataAccess.Configurations;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess;

public class WholesalerContext : DbContext
{
    public WholesalerContext(DbContextOptions<WholesalerContext> options)
    : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Requirement> Requirements { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Workday> Workdays { get; set; }
    public DbSet<WorkTask> WorkTasks { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Delivery> Delivery { get; set; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateDate();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateDate();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new ClientConfiguration())
            .ApplyConfiguration(new RequirementConfiguration())
            .ApplyConfiguration(new PersonConfiguration())
            .ApplyConfiguration(new WorkdayConfiguration())
            .ApplyConfiguration(new WorkTaskConfiguration())
            .ApplyConfiguration(new ActivityConfiguration())
            .ApplyConfiguration(new DeliveryConfiguration());
    }

    private void UpdateDate()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is TrackedEntity);

        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Unchanged || entry.State is EntityState.Detached)
                continue;

            if (entry.State == EntityState.Added)
                ((TrackedEntity)entry.Entity).CreatedDate = DateTime.Now;

            if (entry.State == EntityState.Modified)
                ((TrackedEntity)entry.Entity).UpdatedDate = DateTime.Now;

            if (entry.State == EntityState.Deleted)
                ((TrackedEntity)entry.Entity).DeletedDate = DateTime.Now;
        }
    }
}
