using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
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

        public StorageController(IStorageService serivce, IStorageDtoFactory storageDtoFactory)
        {
            _service = serivce;
            _storageDtoFactory = storageDtoFactory;
        }

        [HttpPost]
        public async Task<ActionResult<StorageDto>> Add([FromBody] AddStorageRequestModel addStorageRequestModel)
        {
            var request = new CreateStorageRequest(addStorageRequestModel.Name);

            var storage = _service.Add(request);
            var storageDto = _storageDtoFactory.Create(storage);

            return storageDto;
        }

        [HttpPatch]
        [Route("{id}/actions/delivery")]
        public async Task<ActionResult<StorageDto>> MushroomsDelivery(Guid id, [FromBody] UpdateStorageRequestModel updateStorageRequestModel)
        {
            var storageDelivery = _service.Delivery(id, updateStorageRequestModel.Quantity);
            var storageDto = _storageDtoFactory.Create(storageDelivery);

            return storageDto;
        }

        [HttpPatch]
        [Route("{id}/actions/departure")]
        public async Task<ActionResult<StorageDto>> MushroomsDeparture(Guid id, [FromBody] UpdateStorageRequestModel updateStorageRequestModel)
        {
            var storageDelivery = _service.Departure(id, updateStorageRequestModel.Quantity);
            var storageDto = _storageDtoFactory.Create(storageDelivery);

            return storageDto;
        }
    }
}
