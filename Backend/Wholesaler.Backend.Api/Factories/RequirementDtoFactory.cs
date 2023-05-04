using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Factories
{
    public class RequirementDtoFactory : IRequirementDtoFactory
    {
        public RequirementDto Create(Requirement requirement)
        {
            return new RequirementDto()
            {
                Id = requirement.Id,
                Quantity = requirement.Quantity,
                ClientId = requirement.ClientId,
                StorageId = requirement.StorageId,
            };
                
        }
    }
}
