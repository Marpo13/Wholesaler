namespace Wholesaler.Backend.DataAccess.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; }
    }
}
