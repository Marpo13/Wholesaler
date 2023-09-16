using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;

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
    }
}
