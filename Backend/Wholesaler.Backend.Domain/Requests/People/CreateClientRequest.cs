namespace Wholesaler.Backend.Domain.Requests.People
{
    public class CreateClientRequest
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }

        public CreateClientRequest(string name, string surname)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
        }
    }
}
