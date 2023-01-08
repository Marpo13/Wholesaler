namespace Wholesaler.Backend.Domain.Entities
{
    public class Requirement
    {
        public Guid Id { get; }

        public Requirement()
        {
            Id = Guid.NewGuid();
        }
    }
}
