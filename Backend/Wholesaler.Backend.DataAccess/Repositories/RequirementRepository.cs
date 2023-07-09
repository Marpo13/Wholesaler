using Wholesaler.Backend.DataAccess.Factories;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using RequirementDb = Wholesaler.Backend.DataAccess.Models.Requirement;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class RequirementRepository : IRequirementRepository
    {
        private readonly IRequirementDbFactory _factory;
        private readonly WholesalerContext _context;

        public RequirementRepository(IRequirementDbFactory factory, WholesalerContext context)
        {
            _factory = factory;
            _context = context;
        }

        public Requirement Add(Requirement requirement)
        {
            var requirementDb = new RequirementDb()
            {
                Id = requirement.Id,
                Quantity = requirement.Quantity,
                ClientId = requirement.ClientId,
                StorageId = requirement.StorageId
            };

            _context.Add(requirementDb);
            _context.SaveChanges();

            return requirement;
        }

        public List<Requirement> GetAll()
        {
            var requirementsDb = _context.Requirements
                .ToList();

            if (requirementsDb == null)
                return new List<Requirement>();

            var requirements = requirementsDb.Select(requirementDb =>
            {
                return _factory.Create(requirementDb);

            }).ToList();

            return requirements;
        }

        public List<Requirement> Get(Guid storageId)
        {
            var requirementsDb = _context.Requirements
                .Where(r => r.StorageId == storageId)
                .ToList();

            if (requirementsDb == null)
                return new List<Requirement>();

            var requirements = requirementsDb.Select(requirementDb =>
            {
                return _factory.Create(requirementDb);

            }).ToList();

            return requirements;
        }

        public Requirement? GetOrDefault(Guid id)
        {
            var requirementDb = _context.Requirements
                .FirstOrDefault(r => r.Id == id);

            if (requirementDb == null) 
                return default;

            var requirement = _factory.Create(requirementDb);

            return requirement;
        }

        public Requirement Update(Requirement requirement)
        {
            var requirementDb = _context.Requirements
                .FirstOrDefault(r => r.Id == requirement.Id);

            if (requirementDb == null)
                throw new EntityNotFoundException($"There is no requirement with id {requirement.Id}");

            requirementDb.Quantity = requirement.Quantity;
            requirementDb.ClientId =requirement.ClientId;
            requirementDb.StorageId = requirement.StorageId;
            requirementDb.Status = requirement.Status;
            requirementDb.DeliveryDate = requirement.DeliveryDate;
            _context.SaveChanges();

            return requirement;
        }

        public List<Requirement> GetCompleted()
        {
            var requirementsDb = _context.Requirements
                .Where(r => r.Status == Status.Completed)
                .ToList();

            if (requirementsDb == null)
                return new List<Requirement>();

            var requirements = requirementsDb.Select(requirementDb =>
            {
                return _factory.Create(requirementDb);

            }).ToList();

            return requirements;
        }

        public List<Requirement> GetOngoing()
        {
            var requirementsDb = _context.Requirements
                .Where(r => r.Status == Status.Ongoing)
                .ToList();

            if (requirementsDb == null)
                return new List<Requirement>();

            var requirements = requirementsDb.Select(requirementDb =>
            {
                return _factory.Create(requirementDb);

            }).ToList();

            return requirements;
        }
    }
}
