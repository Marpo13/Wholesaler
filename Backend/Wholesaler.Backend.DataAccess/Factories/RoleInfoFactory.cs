using Wholesaler.Backend.Domain.Entities.Helpers;
using EmployeeDb = Wholesaler.Backend.DataAccess.Models.Helpers.Employee;
using ManagerDb = Wholesaler.Backend.DataAccess.Models.Helpers.Manager;
using OwnerDb = Wholesaler.Backend.DataAccess.Models.Helpers.Owner;
using RoleDb = Wholesaler.Backend.DataAccess.Models.Helpers.Role;

namespace Wholesaler.Backend.DataAccess.Factories;

public class RoleInfoFactory : IRoleInfoFactory
{
    public RoleDb Create(Role role)
    {
        return role.Type switch
        {
            "Manager" => new ManagerDb
            {
                Name = role.Name,
                Surname = role.Surname
            },
            "Owner" => new OwnerDb
            {
                Name = role.Name,
                Surname = role.Surname
            },
            "Employee" => new EmployeeDb
            {
                Name = role.Name,
                Surname = role.Surname
            },
            _ => throw new NotSupportedException($"Type '{role.Type}' is not supported")
        };
    }
}
