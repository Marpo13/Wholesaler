using Wholesaler.Backend.Domain.Entities.Helpers;
using RoleInfo = Wholesaler.Backend.Domain.Entities.Helpers.Role;

namespace Wholesaler.Backend.Domain.Entities;

public class Person
{
    public Person(Guid id, string login, string password, Role role, string name, string surname)
    {
        Id = id;
        Login = login;
        Password = password;
        Role = role;
        Name = name;
        Surname = surname;
        RoleInfo = GetRoleInfo();
    }

    public Person(string login, string password, Role role, string name, string surname)
    {
        Id = Guid.NewGuid();
        Login = login;
        Password = password;
        Role = role;
        Name = name;
        Surname = surname;
        RoleInfo = GetRoleInfo();
    }

    public Guid Id { get; }
    public string Login { get; }
    public string Password { get; }
    public Role Role { get; }
    public string Name { get; }
    public string Surname { get; }
    public RoleInfo RoleInfo { get; }

    private RoleInfo GetRoleInfo()
    {
        return Role switch
        {
            Role.Manager => new Manager(Name, Surname),
            Role.Owner => new Owner(Name, Surname),
            Role.Employee => new Employee(Name, Surname),
            _ => throw new NotSupportedException("Type is not supported for creating person role.")
        };
    }
}