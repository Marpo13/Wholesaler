using System.Data.Entity;
using Wholesaler.Backend.DataAccess.Configurations;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess
{
    public class WholesalerContext : DbContext
    {
        public DbSet <Client> Clients { get; set; }
        public DbSet <Requirement> Requirements { get; set; }
        public DbSet <WorkTask> Tasks { get; set; }
        public DbSet<Person> People { get; set; }

        public WholesalerContext()
        {
            Database.Connection.ConnectionString = "Server=.\\SQLEXPRESS;Database=Wholesaler;Trusted_Connection=True;";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new RequirementConfiguration());
            modelBuilder.Configurations.Add(new WorkTaskConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
        }
    }
}
