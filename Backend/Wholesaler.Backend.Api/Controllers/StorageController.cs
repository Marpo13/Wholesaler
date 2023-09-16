using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.Storage;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("storages")]

    public class StorageController : ControllerBase
    {
        private readonly IStorageService _service;
        private readonly IStorageDtoFactory _storageDtoFactory;
        private readonly IStorageRepository _repository;

        public StorageController(IStorageService serivce, IStorageDtoFactory storageDtoFactory, IStorageRepository repository)
        {
            _service = serivce;
            _storageDtoFactory = storageDtoFactory;
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<StorageDto>> Add([FromBody] AddStorageRequestModel addStorageRequestModel)
        {
            var request = new CreateStorageRequest(addStorageRequestModel.Name);

            var storage = _service.Add(request);
            var storageDto = _storageDtoFactory.Create(storage);

            return storageDto;
        }

        [HttpGet]
        public async Task<ActionResult<List<StorageDto>>> GetAll()
        {
            var storages = _repository.GetAll();
            var storagesDto = storages
                .Select(s => _storageDtoFactory.Create(s))
                .ToList();

            return storagesDto;
        }

        [HttpPatch]
        [Route("{id}/actions/deliver")]
        public async Task<ActionResult<StorageDto>> MushroomsDelivery(Guid id, [FromBody] UpdateStorageRequestModel updateStorageRequestModel)
        {
            var storageDelivery = _service.Deliver(id, updateStorageRequestModel.Quantity, updateStorageRequestModel.PersonId);
            var storageDto = _storageDtoFactory.Create(storageDelivery);

            return storageDto;
        }
    }
}
