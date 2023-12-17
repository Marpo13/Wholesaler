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

        public ClientController(IClientService clientService, IClientFactory clientFactory, IClientRepository clientRepository)
        {
            _clientService = clientService;
            _clientFactory = clientFactory;
            _clientRepository = clientRepository;
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
            var clients = _clientRepository.GetAll();

            var clientsDto = clients
                .Select(c => _clientFactory.Create(c))
                .ToList();

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
