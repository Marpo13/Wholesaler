using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
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

        public RequirementController(IRequirementService service, IRequirementDtoFactory factory)
        {
            _service = service;
            _factory = factory;
        }

        [HttpPost]
        public async Task<ActionResult<RequirementDto>> Add([FromBody] AddRequirementRequestModel addRequirementRequest)
        {
            var request = new CreateRequirementRequest(
                addRequirementRequest.Quantity,
                addRequirementRequest.ClientId,
                addRequirementRequest.StorageId);

            var requirement = _service.Add(request);

            var requirementDto = _factory.Create(requirement);

            return requirementDto;
        }

        [HttpPatch]
        [Route("{id}/actions/edit")]
        public async Task<ActionResult<RequirementDto>> EditQuantity(Guid id, [FromBody] UdpateRequirementRequestModel updateRequirementRequest)
        {
            var editedRequirement = _service.EditQuantity(id, updateRequirementRequest.Quantity);
            var editedRequirementDto = _factory.Create(editedRequirement);

            return editedRequirementDto;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequirementDto>>> GetAll()
        {
            var requirements = _service.GetAll();
            var requirementsDto = requirements.Select(requirement =>
            {
                var requirementDto = new RequirementDto()
                {
                    Id = requirement.Id,
                    Quantity = requirement.Quantity,
                    ClientId = requirement.ClientId,
                    StorageId = requirement.StorageId,
                };
                return requirementDto;

            }).ToList();

            return requirementsDto;
        }
    }
}
