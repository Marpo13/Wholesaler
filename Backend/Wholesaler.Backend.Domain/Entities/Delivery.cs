namespace Wholesaler.Backend.Domain.Entities
{
    public class Delivery
    {
        public Guid Id { get; }
        public int Quantity { get; }
        public DateTime DeliveryDate { get; }
        public Guid PersonId { get; }

        public Delivery(Guid id, int quantity, DateTime deliveryDate, Guid personId)
        {
            Id = id;
            Quantity = quantity;
            DeliveryDate = deliveryDate;
            PersonId = personId;
        }

        public Delivery(int quantity, DateTime deliveryDate, Guid personId)
        {
            Id = Guid.NewGuid();
            Quantity = quantity;
            DeliveryDate = deliveryDate;
            PersonId = personId;
        }
    }
}
