namespace Wholesaler.Backend.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; }
        public string Name { get; }
        public IReadOnlyList<Requirement> Requirements { get; }

        public Client(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Requirements = new List<Requirement>();
        }
    }
}
