namespace Wholesaler.Backend.DataAccess.Models
{
    public class Requirement
    {
        public Guid Id { get; set; }
        public virtual Client Client { get; set; }
        public Guid ClientId { get; set; }
    }
}
