namespace Wholesaler.Backend.Domain.Entities.Helpers;

public abstract class Role
{
    protected Role(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public abstract string Type { get; }
    public string Name { get; }
    public string Surname { get; }
}
