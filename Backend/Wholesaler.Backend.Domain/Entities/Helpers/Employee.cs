namespace Wholesaler.Backend.Domain.Entities.Helpers;
public class Employee : Role
{
    public Employee(string name, string surname)
        : base(name, surname)
    {
    }

    public override string Type => "Employee";
}
