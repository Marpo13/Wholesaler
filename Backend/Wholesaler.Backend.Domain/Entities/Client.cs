namespace Wholesaler.Backend.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }

        public Client(Guid id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }

        public Client(string name, string surname)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;       
        }
    }
}
