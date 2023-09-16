using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain.Interfaces;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("raports")]
    public class RaportController : ControllerBase
    {
        private readonly IRaportService _raportService;

        public RaportController(IRaportService raportService)
        {
            _raportService = raportService;
        }

        [HttpGet]
        [Route("costs")]
        public async Task<ActionResult<float>> GetCosts()
        {
            var costs = _raportService.GetCosts();

            return costs;
        }

        [HttpGet]
        [Route("costs/employee/{id}")]
        public async Task<ActionResult<float>> GetCostsForEmployee(Guid id)
        {
            var costsForEmployee = _raportService.GetCostsForEmployee(id);

            return costsForEmployee;
        }
    }
}
