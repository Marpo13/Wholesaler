using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
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
            {
                return new List<Requirement>();
            }

            return requirementsDb
                .Select(requirementDb => _factory.Create(requirementDb))
                .ToList();
        }

        public List<Requirement> Get(Guid storageId)
        {
            var requirementsDb = _context.Requirements
                .Where(r => r.StorageId == storageId)
                .ToList();

            if (requirementsDb == null)
            {
                return new List<Requirement>();
            }

            return requirementsDb
                .Select(requirementDb => _factory.Create(requirementDb))
                .ToList();
        }

        public Requirement? GetOrDefault(Guid id)
        {
            var requirementDb = _context.Requirements
                .FirstOrDefault(r => r.Id == id);

            if (requirementDb == null)
            {
                return default;
            }

            return _factory.Create(requirementDb);
        }

        public Requirement Update(Requirement requirement)
        {
            var requirementDb = _context.Requirements
                .FirstOrDefault(r => r.Id == requirement.Id)
                ?? throw new EntityNotFoundException($"There is no requirement with id {requirement.Id}");

            requirementDb.Quantity = requirement.Quantity;
            requirementDb.ClientId = requirement.ClientId;
            requirementDb.StorageId = requirement.StorageId;
            requirementDb.Status = requirement.Status;
            requirementDb.DeliveryDate = requirement.DeliveryDate;
            _context.SaveChanges();

            return requirement;
        }

        public List<Requirement> GetByStatus(string status)
        {
            var statusName = PrepareStatusName(status);

            if (!Enum.TryParse(statusName, out Status requirementStatus))
            {
                throw new InvalidDataProvidedException("You entered an invalid value of status.");
            }

            var requirementsDb = _context.Requirements
                .Where(r => r.Status == requirementStatus)
                .ToList();

            if (requirementsDb == null)
            {
                return new List<Requirement>();
            }

            return requirementsDb
                .Select(requirementDb => _factory.Create(requirementDb))
                .ToList();
        }

        public async Task<(List<Requirement>, List<string>)> GetByCustomFiltersAsync(Dictionary<string, string> customFilters)
        {
            var query = _context.Requirements
                .AsQueryable();

            var errors = new List<string>();

            foreach (var filter in customFilters)
            {
                var property = typeof(Requirement).GetProperty(filter.Key);

                if (property == null)
                {
                    errors.Add($"Name {filter.Key} is invalid.");
                    continue;
                }

                var convertionValid = TryParseValue(filter.Value, property.PropertyType);
                if (!convertionValid)
                {
                    errors.Add($"Value {filter.Value} for property {filter.Key} is invalid.");
                    continue;
                }

                query = query
                    .Where($"{filter.Key} == @0", filter.Value);
            }

            var requirements = await query
                .Select(requirementDb => _factory.Create(requirementDb))
                .ToListAsync();

            return (requirements, errors);
        }

        private bool TryParseValue(string valueToConvert, Type propertyType)
        {
            if (propertyType == typeof(int))
            {
                return int.TryParse(valueToConvert, out var parsed);
            }

            if (propertyType == typeof(Guid))
            {
                return Guid.TryParse(valueToConvert, out var parsed);
            }

            if (propertyType == typeof(DateTime))
            {
                return DateTime.TryParse(valueToConvert, out var parsed);
            }

            if (propertyType == typeof(Status))
            {
                var statusName = PrepareStatusName(valueToConvert);
                return Enum.TryParse(statusName, out Status requirementStatus);
            }

            return false;
        }

        private static string PrepareStatusName(string status)
        {
            return char.ToUpper(status[0])
                + status.Substring(1)
                .ToLower();
        }
    }
}
