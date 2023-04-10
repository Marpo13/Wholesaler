namespace Wholesaler.Core.Dto.ResponseModels
{
    public class RequirementDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid ClientId { get; set; }
    }
}
