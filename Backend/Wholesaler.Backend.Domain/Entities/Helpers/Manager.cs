namespace Wholesaler.Backend.Domain.Entities.Helpers;
public class Manager : Role
{
    public Manager(string name, string surname)
        : base(name, surname)
    {
    }

    public override string Type => "Manager";
}
