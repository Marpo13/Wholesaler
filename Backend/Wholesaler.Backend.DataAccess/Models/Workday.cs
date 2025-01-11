namespace Wholesaler.Backend.DataAccess.Models;

public class Workday
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime? Stop { get; set; }
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
}
