namespace Wholesaler.Backend.Domain.Entities;

public class Activity
{
    public Activity(Guid id, DateTime start, DateTime? stop, Guid personId)
    {
        Id = id;
        Start = start;
        Stop = stop;
        PersonId = personId;
    }

    public Guid Id { get; }
    public DateTime Start { get; }
    public DateTime? Stop { get; private set; }
    public Guid PersonId { get; }

    public bool IsOpen => Stop == null;

    public void Close(DateTime time)
    {
        Stop = time;
    }
}
