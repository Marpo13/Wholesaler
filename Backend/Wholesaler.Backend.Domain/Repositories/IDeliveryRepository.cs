using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IDeliveryRepository
    {
        Delivery Add(Delivery delivery);
        List<Delivery> GetAll();
    }
}
