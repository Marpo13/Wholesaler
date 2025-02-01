namespace Wholesaler.Core.Dto.ResponseModels;

public class WorkdayDto
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime? Stop { get; set; }
}
