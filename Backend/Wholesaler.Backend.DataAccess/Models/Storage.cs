namespace Wholesaler.Backend.DataAccess.Models;

public class Storage
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int State { get; set; }
    public virtual ICollection<Requirement>? Requirements { get; set; }
}
