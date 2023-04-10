namespace Wholesaler.Backend.Domain.Requests.Requirements
{
    public class CreateRequirementRequest
    {
        public Guid Id { get; }
        public int Quantity { get; }
        public Guid ClientId { get; }

        public CreateRequirementRequest(int quantity, Guid clientId)
        {
            Id = Guid.NewGuid();
            Quantity = quantity;
            ClientId = clientId;
        }
    }
}
