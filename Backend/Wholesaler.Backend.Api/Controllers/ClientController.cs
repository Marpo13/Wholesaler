using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.People;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IClientFactory _clientFactory;
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, IClientFactory clientFactory, IClientRepository clientRepository, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _clientFactory = clientFactory;
            _clientRepository = clientRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Dupa()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5050");

            var test = new[] { 1, 2, 3, 4, 5, 6 };

            var tasks = test.Select(async t =>
            {
                await httpClient.GetAsync("/clients");
            });

            await Task.WhenAll(tasks);

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<ClientDto>> Add([FromBody] AddClientRequestModel addClientRequest)
        {
            var request = new CreateClientRequest(
                addClientRequest.Name, 
                addClientRequest.Surname);

            var client = _clientService.Add(request);            

            return _clientFactory.Create(client);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _clientService.Delete(id);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetAll()
        {
            _logger.LogInformation("Start");

            var clients = _clientRepository.GetAll();

            var clientsDto = clients
                .Select(c => _clientFactory.Create(c))
                .ToList();

            _logger.LogInformation("Stop");

            return clientsDto;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ClientDto>> Get(Guid id)
        {
            var client = _clientRepository.Get(id);

            return _clientFactory.Create(client);
        }
    }
}
