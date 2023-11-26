namespace Wholesaler.Core.Dto.RequestModels
{
    public class UpdateStorageRequestModel
    {
        public int Quantity { get; set; }
        public Guid PersonId { get; set; }
    }
}
