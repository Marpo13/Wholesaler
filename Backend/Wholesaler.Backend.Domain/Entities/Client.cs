namespace Wholesaler.Backend.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public IReadOnlyList<Requirement>? Requirements { get; }

        public Client(Guid id, string name, string surname, IReadOnlyList<Requirement> requirements)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Requirements = requirements;
        }

        public Client(string name, string surname)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Requirements = new List<Requirement>();            
        }
    }
}
