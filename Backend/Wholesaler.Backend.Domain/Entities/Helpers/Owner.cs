namespace Wholesaler.Backend.Domain.Entities.Helpers;
public class Owner : Role
{
    public Owner(string name, string surname)
        : base(name, surname)
    {
    }

    public override string Type => "Owner";
}
