namespace Wholesaler.Backend.Domain.Entities
{
    public class Requirement
    {
        public Guid Id { get; set; }

        public Requirement()
        {
            Id = Guid.NewGuid();
        }
    }
}
