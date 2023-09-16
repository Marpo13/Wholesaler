using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;
using DeliveryDb = Wholesaler.Backend.DataAccess.Models.Delivery;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly WholesalerContext _context;

        public DeliveryRepository(WholesalerContext context)
        {
            _context = context;
        }

        public Delivery Add(Delivery delivery)
        {
            var deliveryDb = new DeliveryDb()
            {
                Id = delivery.Id,
                Quantity = delivery.Quantity,
                DeliveryDate = delivery.DeliveryDate,
                PersonId = delivery.PersonId,
            };

            _context.Add(deliveryDb);
            _context.SaveChanges();

            return delivery;
        }

        public List<Delivery> GetAll()
        {
            var deliveriesDb = _context.Delivery
                .ToList();

            if (deliveriesDb.Any())
            {
                var delivery = deliveriesDb.Select(deliveryDb =>
                {
                    return new Delivery(
                        deliveryDb.Id,
                        deliveryDb.Quantity,
                        deliveryDb.DeliveryDate,
                        deliveryDb.PersonId);
                });

                return delivery.ToList();
            }

            return new List<Delivery>();
        }
    }
}
