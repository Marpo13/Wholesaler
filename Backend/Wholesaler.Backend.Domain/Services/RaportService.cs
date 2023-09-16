using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;

namespace Wholesaler.Backend.Domain.Services
{
    public class RaportService : IRaportService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly float _multiplier = 0.093f;

        public RaportService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public float GetCosts()
        {
            var quantity = 0;
            var deliveries = _deliveryRepository.GetAll();

            foreach (var delivery in deliveries)
                quantity += delivery.Quantity;

            var costs = _multiplier * quantity;

            return costs;
        }
    }
}
