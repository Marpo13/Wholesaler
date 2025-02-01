namespace Wholesaler.Core.Dto.ResponseModels;

public class WorkTaskDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public int Row { get; set; }
    public bool IsStarted { get; set; }
    public bool IsFinished { get; set; }
}
