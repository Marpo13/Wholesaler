namespace Wholesaler.Backend.DataAccess.Models
{
    public class WorkTask
    {
        public Guid Id { get; set; }
        public int Row { get; set; }
        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }
        public virtual ICollection<Activity>? Activities { get; set; }
        public virtual Person? Person { get; set; }
        public Guid? PersonId { get; set; }
    }
}
