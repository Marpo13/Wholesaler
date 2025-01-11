using Wholesaler.Backend.Domain.Entities;
using RequirementDb = Wholesaler.Backend.DataAccess.Models.Requirement;

namespace Wholesaler.Backend.DataAccess.Factories;

public interface IRequirementDbFactory
{
    Requirement Create(RequirementDb requirement);
}
