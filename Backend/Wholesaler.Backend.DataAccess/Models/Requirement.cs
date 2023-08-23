using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.DataAccess.Models
{
    public class Requirement : TrackedEntity
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public virtual Client Client { get; set; }
        public Guid ClientId { get; set; }
        public virtual Storage Storage { get; set; }
        public Guid StorageId { get; set; }
        public Status Status { get; set; }
        public DateTime? DeliveryDate { get; set; } = null;
    }
}
