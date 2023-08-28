using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.Requirements;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("requirements")]
    public class RequirementController : ControllerBase
    {
        private readonly IRequirementService _service;
        private readonly IRequirementDtoFactory _factory;
        private readonly IRequirementRepository _requirementRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IStorageRepository _storageRepository;

        public RequirementController(IRequirementService service, IRequirementDtoFactory factory, IRequirementRepository requirementRepository, IClientRepository clientRepository, 
            IStorageRepository storageRepository)
        {
            _service = service;
            _factory = factory;
            _requirementRepository = requirementRepository;
            _clientRepository = clientRepository;
            _storageRepository = storageRepository;
        }

        [HttpPost]
        public async Task<ActionResult<RequirementDto>> Add([FromBody] AddRequirementRequestModel addRequirementRequest)
        {
            var client = _clientRepository.Get(addRequirementRequest.ClientId);
            var storage = _storageRepository.Get(addRequirementRequest.StorageId);

            var request = new CreateRequirementRequest(
                addRequirementRequest.Quantity,
                client.Id,
                storage.Id);

            var requirement = _service.Add(request);

            var requirementDto = _factory.Create(requirement);

            return requirementDto;
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<RequirementDto>> EditQuantity(Guid id, [FromBody] UdpateRequirementRequestModel updateRequirementRequest)
        {
            var editedRequirement = _service.EditQuantity(id, updateRequirementRequest.Quantity);
            var editedRequirementDto = _factory.Create(editedRequirement);

            return editedRequirementDto;
        }


        [HttpPatch]
        [Route("{id}/actions/complete")]
        public async Task<ActionResult<RequirementDto>> Complete(Guid id)
        {
            var completedRequirement = _service.Complete(id);
            var completedRequirementDto = _factory.Create(completedRequirement);

            return completedRequirementDto;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequirementDto>>> GetAll()
        {
            var requirements = _requirementRepository.GetAll();
            var requirementsDto = requirements
                .Select(r => _factory.Create(r))
                .ToList();

            return requirementsDto;
        }

        [HttpGet]
        [Route("withStorageId")]
        public async Task<ActionResult<List<RequirementDto>>> Get([FromQuery] Guid storageId)
        {
            var requirements = _requirementRepository.Get(storageId);
            var requirementsDto = requirements
                .Select(r => _factory.Create(r))
                .ToList();

            return requirementsDto;
        }

        [HttpGet]
        [Route("byStatus")]
        public async Task<ActionResult<List<RequirementDto>>> GetByStatus([FromQuery] string status)
        {
            var requirements = _requirementRepository.GetByStatus(status);
            var requirementsDto = requirements
                .Select(r => _factory.Create(r))
                .ToList();

            return requirementsDto;
        }

        [HttpGet]
        [Route("incomes")]
        public async Task<ActionResult<int>> GetIncomes()
        {
            var quantity = _service.GetSumOfCompletedRequirements();
            var incomes = _service.GetIncomes(quantity);

            return incomes;
        }
    }
}
