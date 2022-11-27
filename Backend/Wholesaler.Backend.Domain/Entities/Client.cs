namespace Wholesaler.Backend.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Requirement> Requirements { get; set; }

        public Client(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
