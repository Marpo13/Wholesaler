namespace Wholesaler.Backend.DataAccess.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Stop { get; set; }
        public virtual WorkTask WorkTask { get; set; }
        public Guid WorkTaskId { get; set; }
        public virtual Person Person { get; set; }
        public Guid PersonId { get; set; }
    }
}
