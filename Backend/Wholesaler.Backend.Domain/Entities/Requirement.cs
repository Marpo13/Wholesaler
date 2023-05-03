namespace Wholesaler.Backend.Domain.Entities
{
    public class Requirement
    {
        public Guid Id { get; }
        public int Quantity { get; }
        public Guid ClientId { get; }
        public Guid StorageId { get; }

        public Requirement(Guid id, int quantity, Guid clientId, Guid storageId)
        {
            Id = id;
            Quantity = quantity;
            ClientId = clientId;
            StorageId = storageId;
        }

        public Requirement(int quantity, Guid clientId, Guid storageId)
        {
            Id = Guid.NewGuid();
            Quantity = quantity;
            ClientId = clientId;
            StorageId = storageId;
        }
    }
}
