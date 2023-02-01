using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.DataAccess.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Workday> Workdays { get; set; }
        public ICollection<WorkTask> WorkTasks { get; set; } 
        
    }
}
