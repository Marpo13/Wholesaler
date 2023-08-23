using Microsoft.EntityFrameworkCore;
using Wholesaler.Backend.DataAccess.Configurations;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess
{
    public class WholesalerContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Workday> Workdays { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public WholesalerContext(DbContextOptions<WholesalerContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new RequirementConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new WorkdayConfiguration());
            modelBuilder.ApplyConfiguration(new WorkTaskConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityConfiguration());
        }

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

        private void UpdateDate()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is TrackedEntity);

            foreach (var entry in entries)
            {
                if (entry.State is EntityState.Unchanged)
                    continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        ((TrackedEntity)entry.Entity).CreatedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        ((TrackedEntity)entry.Entity).UpdatedDate = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        ((TrackedEntity)entry.Entity).DeletedDate = DateTime.Now;
                        break;
                }
            }
        }
    }
}
