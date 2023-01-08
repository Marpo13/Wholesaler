using Microsoft.EntityFrameworkCore;
using System;
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
        }
    }
}
