using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
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

        public ClientController(IClientService clientService, IClientFactory clientFactory)
        {
            _clientService = clientService;
            _clientFactory = clientFactory;
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
            var clients = _clientService.GetAll();

            var clientsDto = clients.Select(client =>
            {
                var clientDto = new ClientDto()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                };

                return clientDto;
            });

            return clientsDto.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ClientDto>> Get(Guid id)
        {
            var client = _clientService.Get(id);

            return _clientFactory.Create(client);
        }
    }
}
