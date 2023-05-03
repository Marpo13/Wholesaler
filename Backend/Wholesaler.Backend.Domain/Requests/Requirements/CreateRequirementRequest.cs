namespace Wholesaler.Backend.Domain.Requests.Requirements
{
    public class CreateRequirementRequest
    {
        public Guid Id { get; }
        public int Quantity { get; }
        public Guid ClientId { get; }
        public Guid StorageId { get; }

        public CreateRequirementRequest(int quantity, Guid clientId, Guid storageId)
        {
            Id = Guid.NewGuid();
            Quantity = quantity;
            ClientId = clientId;
            StorageId = storageId;
        }
    }
}
