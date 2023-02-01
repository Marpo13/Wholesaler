namespace Wholesaler.Backend.DataAccess.Models
{
    public class WorkTask
    {
        public Guid Id { get; set; }
        public int Row { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Stop { get; set; }
        public virtual Person? Person { get; set; }
        public Guid? PersonId { get; set; }
    }
}
