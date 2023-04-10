using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Requests.Requirements;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("requirement")]
    public class RequirementController : ControllerBase
    {
        private readonly IRequirementService _requirementService;

        public RequirementController(IRequirementService requirementService)
        {
            _requirementService = requirementService;
        }

        [HttpPost]
        public async Task<ActionResult<RequirementDto>> Add([FromBody] AddRequirementRequestModel addRequirementRequest)
        {
            var request = new CreateRequirementRequest(
                addRequirementRequest.Quantity,
                addRequirementRequest.ClientId);

            var requirement = _requirementService.Add(request);
            var requirementDto = new RequirementDto()
            {
                Id = requirement.Id,
                Quantity = requirement.Quantity,
                ClientId = requirement.ClientId,
            };

            return requirementDto;
        }
    }
}
