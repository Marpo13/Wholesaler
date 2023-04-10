using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;
using RequirementDb = Wholesaler.Backend.DataAccess.Models.Requirement;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class RequirementRepository : IRequirementRepository
    {
        private readonly WholesalerContext _context;

        public RequirementRepository(WholesalerContext context)
        {
            _context = context;
        }

        public Requirement Add(Requirement requirement)
        {
            var requirementDb = new RequirementDb()
            {
                Id = requirement.Id,
                Quantity = requirement.Quantity,
                ClientId = requirement.ClientId,
            };

            _context.Add(requirementDb);
            _context.SaveChanges();

            return requirement;
        }
    }
}
