namespace Wholesaler.Backend.DataAccess.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Requirement>? Requirements { get; set; }
    }
}
