using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Requests.Requirements;

namespace Wholesaler.Backend.Domain.Factories
{
    public class RequirementFactory : IRequirementFactory
    {
        public Requirement Create(CreateRequirementRequest request)
        {
            if(request.Quantity == 0)
                throw new InvalidDataProvidedException("You need to provide quantity more than 0.");
            if(request.Quantity < 0)
                throw new InvalidDataProvidedException("You need to provide quantity more than 0.");
            if(request.ClientId == Guid.Empty)
                throw new InvalidDataProvidedException("You need to provide quantity client.");

            var requirement = new Requirement(
                request.Quantity, 
                request.ClientId);

            return requirement;
        }
    }
}
