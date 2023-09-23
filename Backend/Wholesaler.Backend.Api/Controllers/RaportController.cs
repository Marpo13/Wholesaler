using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Requests.Delivery;

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
        public async Task<ActionResult<float>> GetCostsDeclaredByTimespan(long from, long to)
        {
            var fromDate = DateTimeOffset.FromUnixTimeMilliseconds(from);
            var toDate = DateTimeOffset.FromUnixTimeMilliseconds(to);
            var costs = _raportService.GetCosts(fromDate, toDate);

            return costs;
        }
    }
}
