namespace Wholesaler.Backend.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; }
        public DateTime Start { get; }
        public DateTime? Stop { get; private set; }
        public Guid PersonId { get; }

        public bool IsOpen => Stop == null;

        public Activity(Guid id, DateTime start, DateTime? stop, Guid personId)
        {
            Id = id;
            Start = start;
            Stop = stop;
            PersonId = personId;
        }

        public Activity(Guid id, DateTime start, Guid personId)
        {
            Id = id;
            Start = start;
            PersonId = personId;
        }

        public void Close()
        {
            Stop = DateTime.Now;
        }
    }
}
