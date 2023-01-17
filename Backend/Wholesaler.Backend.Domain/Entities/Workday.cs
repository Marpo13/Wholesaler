namespace Wholesaler.Backend.Domain.Entities
{
    public class Workday
    {
        public Guid Id { get; }
        public DateTime Start { get; }
        public DateTime? Stop { get; private set; }
        public Person Person { get; }

        public Workday(Guid id, DateTime start, DateTime? stop, Person person)
        {
            Id = id;
            Start = start;
            Stop = stop;
            Person = person;
        }
        public Workday(DateTime start, Person person)
        {
            Id = Guid.NewGuid();
            Start = start;
            Person = person;
        }

        public void StopWorkday()
        {
            var time = DateTime.Now;
            Stop = time;
        }
    }
}
