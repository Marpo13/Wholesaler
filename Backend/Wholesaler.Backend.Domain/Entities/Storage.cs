namespace Wholesaler.Backend.Domain.Entities;

public class Storage
{
    public Storage(string name)
    {
        Id = Guid.NewGuid();
        State = 0;
        Name = name;
    }

    public Storage(int state, string name)
    {
        Id = Guid.NewGuid();
        State = state;
        Name = name;
    }

    public Storage(Guid id, int state, string name)
    {
        Id = id;
        State = state;
        Name = name;
    }

    public Guid Id { get; }
    public string Name { get; }
    public int State { get; private set; }

    public void SetState(int state)
    {
        State = state;
    }
}
