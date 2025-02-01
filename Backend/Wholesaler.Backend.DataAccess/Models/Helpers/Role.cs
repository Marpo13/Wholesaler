namespace Wholesaler.Backend.DataAccess.Models.Helpers;

public abstract class Role
{
    public abstract string Type { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
