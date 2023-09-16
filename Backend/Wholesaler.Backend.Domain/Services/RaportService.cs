using Wholesaler.Backend.Domain.Entities;
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
            var deliveries = _deliveryRepository.GetAll();
            var costs = GetCosts(deliveries);

            return costs;
        }

        public float GetCostsForEmployee(Guid employeeId)
        {
            var deliveries = _deliveryRepository.GetForEmployee(employeeId);
            var costs = GetCosts(deliveries);

            return costs;
        }

        private float GetCosts(List<Delivery> deliveries)
        {
            var quantity = 0;

            foreach (var delivery in deliveries)
                quantity += delivery.Quantity;

            var costs = _multiplier * quantity;
            return costs;
        }
    }
}
