namespace Wholesaler.Core.Dto.RequestModels
{
    public class ChangeOwnerRequestModel
    {
        public Guid WorkTaskId { get; set; }
        public Guid NewOwnerId { get; set; }
    }
}
