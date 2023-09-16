using Wholesaler.Backend.Domain.Entities;
using DeliveryDb = Wholesaler.Backend.DataAccess.Models.Delivery;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public class DeliveryFactory : IDeliveryFactory
    {
        public List<Delivery> Create(List<DeliveryDb> deliveriesDb)
        {
            return deliveriesDb.Select(deliveryDb =>
            {
                return new Delivery(
                    deliveryDb.Id,
                    deliveryDb.Quantity,
                    deliveryDb.DeliveryDate,
                    deliveryDb.PersonId);

            }).ToList();
        }
    }
}
