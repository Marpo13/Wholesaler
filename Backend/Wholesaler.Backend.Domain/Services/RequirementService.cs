using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Providers.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.Requirements;
using Wholesaler.Backend.Domain.Responses.Requirements;

namespace Wholesaler.Backend.Domain.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementFactory _factory;
        private readonly IRequirementRepository _repository;
        private readonly IStorageService _storageService;
        private readonly ITransaction _transaction;
        private readonly ITimeProvider _timeProvider;

        public RequirementService(IRequirementFactory requirementFactory,
            IRequirementRepository requirementRepository,
            IStorageService storageService,
            ITransaction transaction,
            ITimeProvider timeProvider)
        {
            _factory = requirementFactory;
            _repository = requirementRepository;
            _storageService = storageService;
            _transaction = transaction;
            _timeProvider = timeProvider;
        }

        public Requirement Add(CreateRequirementRequest request)
        {
            var requirement = _factory.Create(request);
            _repository.Add(requirement);

            return requirement;
        }

        public Requirement EditQuantity(Guid id, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidDataProvidedException("Quantity must be more than 0.");

            var requirement = _repository.GetOrDefault(id);
            if (requirement == null)
                throw new EntityNotFoundException($"There is no requirement with id {id}.");
            if (requirement.Status == Status.Completed)
                throw new InvalidDataProvidedException($"Requirement with id {id} is completed.");

            requirement.UpdateQuantity(quantity);
            _repository.Update(requirement);

            return requirement;
        }

        public Requirement Complete(Guid id)
        {
            var time = _timeProvider.Now();

            _transaction.Begin();

            var requirement = _repository.GetOrDefault(id);
            if (requirement == null)
            {
                throw new EntityNotFoundException($"There is no requirement with id {id}.");
            }
            if (requirement.Status == Status.Completed)
            {
                throw new InvalidDataProvidedException($"Requirement with id {id} is completed.");
            }

            requirement.Complete();
            requirement.SetDate(time);

            _repository.Update(requirement);
            _storageService.Depart(requirement.StorageId, requirement);

            _transaction.Commit();

            return requirement;
        }

        public async Task<GetByCustomFiltersResponse> GetByCustomFiltersAsync(Dictionary<string, string> customFilters)
        {
            var errors = new List<string>();
            var validFilters = new Dictionary<string, string>();

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

                validFilters.Add(filter.Key, filter.Value);
            }

            var requirements = await _repository
                .GetByCustomFiltersAsync(validFilters);
        
            return new GetByCustomFiltersResponse(requirements, errors);
        }

        private static bool TryParseValue(string valueToConvert, Type propertyType)
        {
            if (propertyType == typeof(int))
            {
                return int.TryParse(valueToConvert, out var _);
            }

            if (propertyType == typeof(Guid))
            {
                return Guid.TryParse(valueToConvert, out var _);
            }

            if (propertyType == typeof(DateTime))
            {
                return DateTime.TryParse(valueToConvert, out var _);
            }

            if (propertyType == typeof(Status))
            {
                var statusName = PrepareStatusName(valueToConvert);
                return Enum.TryParse(statusName, out Status _);
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
