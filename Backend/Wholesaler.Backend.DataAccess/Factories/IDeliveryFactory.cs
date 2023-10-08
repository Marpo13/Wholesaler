using Wholesaler.Backend.Domain.Entities;
using DeliveryDb = Wholesaler.Backend.DataAccess.Models.Delivery;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public interface IDeliveryFactory
    {
        Delivery Create(DeliveryDb deliveryDb);
    }
}
