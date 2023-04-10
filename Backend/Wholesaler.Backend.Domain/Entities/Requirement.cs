namespace Wholesaler.Backend.Domain.Entities
{
    public class Requirement
    {
        public Guid Id { get; }
        public int Quantity { get; }
        public Guid ClientId { get; }

        public Requirement(Guid id, int quantity, Guid clientId)
        {
            Id = id;
            Quantity = quantity;
            ClientId = clientId;
        }

        public Requirement(int quantity, Guid clientId)
        {
            Id = Guid.NewGuid();
            Quantity = quantity;
            ClientId = clientId;
        }
    }
}
