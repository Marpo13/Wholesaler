using Wholesaler.Backend.Domain.Entities.Helpers;
using RoleDb = Wholesaler.Backend.DataAccess.Models.Helpers.Role;

namespace Wholesaler.Backend.DataAccess.Factories;

public interface IRoleInfoFactory
{
    RoleDb Create(Role role);
}
