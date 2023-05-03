namespace Wholesaler.Backend.DataAccess.Models
{
    public class Requirement
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public virtual Client Client { get; set; }
        public Guid ClientId { get; set; }
        public virtual Storage Storage { get; set; }
        public Guid StorageId { get; set; }
    }
}
