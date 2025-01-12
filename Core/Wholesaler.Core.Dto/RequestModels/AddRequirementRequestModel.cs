namespace Wholesaler.Core.Dto.RequestModels;

public class AddRequirementRequestModel
{
    public int Quantity { get; set; }
    public Guid ClientId { get; set; }
    public Guid StorageId { get; set; }
}
