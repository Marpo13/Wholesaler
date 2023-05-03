using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Requests.Storage
{
    public class CreateStorageRequest
    {
        public Guid Id { get; }
        public int State { get; }
        public string Name { get; }
        public ICollection<Requirement>? Requirements { get; }

        public CreateStorageRequest(string name)
        {
            Id = Guid.NewGuid();
            State = 0;
            Name = name;
            Requirements = new List<Requirement>();
        }
    }
}
