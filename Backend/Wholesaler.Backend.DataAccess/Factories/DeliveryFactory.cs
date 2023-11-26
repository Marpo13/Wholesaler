using Wholesaler.Backend.Domain.Entities;
using DeliveryDb = Wholesaler.Backend.DataAccess.Models.Delivery;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public class DeliveryFactory : IDeliveryFactory
    {
        public Delivery Create(DeliveryDb deliveryDb)
        {
            return new Delivery(
                    deliveryDb.Id,
                    deliveryDb.Quantity,
                    deliveryDb.DeliveryDate,
                    deliveryDb.PersonId);
        }
    }
}
