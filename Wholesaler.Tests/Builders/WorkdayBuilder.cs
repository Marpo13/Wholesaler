using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Tests.Builders;

public class WorkdayBuilder
{
    private readonly Guid _id;
    private DateTime _start;
    private DateTime? _stop;
    private Guid _personId;

    public WorkdayBuilder()
    {
        _id = Guid.NewGuid();
        _start = new(2023, 02, 13, 12, 0, 0);
        _stop = null;
        _personId = Guid.NewGuid();
    }

    public Workday Build()
    {
        return new()
        {
            Id = _id,
            Start = _start,
            Stop = _stop,
            PersonId = _personId
        };
    }

    public WorkdayBuilder WithStartTime(DateTime time)
    {
        _start = time;
        return this;
    }

    public WorkdayBuilder WithStopTime(DateTime time)
    {
        _stop = time;
        return this;
    }

    public WorkdayBuilder WithPersonId(Guid personId)
    {
        _personId = personId;
        return this;
    }
}
