namespace Wholesaler.Backend.DataAccess.Models;

public class TrackedEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
