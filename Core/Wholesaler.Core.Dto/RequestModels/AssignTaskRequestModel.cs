namespace Wholesaler.Core.Dto.RequestModels
{
    public class AssignTaskRequestModel
    {
        public Guid UserId { get; set; }
        public Guid WorkTaskId { get; set; }
    }
}
