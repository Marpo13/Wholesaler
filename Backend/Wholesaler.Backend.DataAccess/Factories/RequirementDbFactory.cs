using Wholesaler.Backend.Domain.Entities;
using RequirementDb = Wholesaler.Backend.DataAccess.Models.Requirement;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public class RequirementDbFactory : IRequirementDbFactory
    {
        public Requirement Create(RequirementDb requirement)
        {
            return new Requirement(
                requirement.Id,
                requirement.Quantity,
                requirement.ClientId,
                requirement.StorageId,
                requirement.Status,
                requirement.DeliveryDate);
        }
    }
}
