using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Tests.Builders;

public class StorageBuilder
{
    private Guid _id;
    private string _name;
    private int _state;

    public StorageBuilder()
    {
        Refresh();
    }

    public void Refresh()
    {
        _id = Guid.NewGuid();
        _name = $"{Guid.NewGuid()}";
        _state = 100;
    }

    public Storage Build()
    {
        var storage = new Storage()
        {
            Id = _id,
            Name = _name,
            State = _state
        };

        Refresh();

        return storage;
    }
}
