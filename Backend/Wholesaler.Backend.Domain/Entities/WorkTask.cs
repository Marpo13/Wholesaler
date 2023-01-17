namespace Wholesaler.Backend.Domain.Entities
{
    public class WorkTask
    {
        public Guid Id { get; }
        public int Row { get; }
        public DateTime? Start { get; private set; }
        public DateTime? Stop { get; private set; }
        public Person? Person { get; private set; }
       
        public WorkTask(Guid id, int row, DateTime? start, DateTime? stop, Person? person)
        {
            Id = id;
            Row = row;
            Start = start;
            Stop = stop;
            Person = person;
        }

        public WorkTask(int row)
        {
            Id = Guid.NewGuid();
            Row = row;            
        }
        public WorkTask(Guid id, int row)
        {
            Id = id;
            Row = row;
        }

        public void AssignPerson(Person person)
        {
            Person = person;
        }

        public void StartWorkTask()
        {
            var time = DateTime.Now;
            Start = time;
        }
        
        public void StopWorkTask()
        {
            var time = DateTime.Now;
            Stop = time;
        }
    }
}
