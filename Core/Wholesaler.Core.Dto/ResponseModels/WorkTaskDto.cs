namespace Wholesaler.Core.Dto.ResponseModels
{
    public class WorkTaskDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Row { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Stop { get; set; }
    }
}
