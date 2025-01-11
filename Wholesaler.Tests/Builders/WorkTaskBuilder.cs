using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Tests.Builders;

public class WorkTaskBuilder
{
    private readonly Guid _id;
    private int _row;
    private bool _isStarted;
    private bool _isFinished;
    private Guid? _personId;

    public WorkTaskBuilder()
    {
        _id = Guid.NewGuid();
        _row = 1;
        _isStarted = false;
        _isFinished = false;
        _personId = null;
    }

    public WorkTask Build()
    {
        return new()
        {
            Id = _id,
            Row = _row,
            IsStarted = _isStarted,
            IsFinished = _isFinished,
            PersonId = _personId
        };
    }

    public WorkTaskBuilder WithRow(int rowNumber)
    {
        _row = rowNumber;
        return this;
    }

    public WorkTaskBuilder WithPersonId(Guid personId)
    {
        _personId = personId;
        return this;
    }

    public WorkTaskBuilder WithStartedStatus()
    {
        _isStarted = true;
        return this;
    }

    public WorkTaskBuilder WithFinishedStatus()
    {
        _isFinished = true;
        return this;
    }
}
