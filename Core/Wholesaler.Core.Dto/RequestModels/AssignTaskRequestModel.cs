namespace Wholesaler.Core.Dto.RequestModels
{
    public class AssignTaskRequestModel
    {
        public Guid WorkTaskId { get; set; }
        public Guid UserId { get; set; }            
    }
}
