using Wholesaler.Backend.DataAccess.Models;
using Status = Wholesaler.Backend.Domain.Entities.Status;

namespace Wholesaler.Tests.Builders;

public class RequirementBuilder
{
    private Guid _id;
    private int _quantity;
    private Guid _clientId;
    private Guid _storageId;
    private Status _status;
    private DateTime? _deliveryDate;

    public RequirementBuilder()
    {
        Refresh();
    }

    public void Refresh()
    {
        _id = Guid.NewGuid();
        _quantity = 20;
        _clientId = Guid.NewGuid();
        _storageId = Guid.NewGuid();
        _status = Status.Ongoing;
        _deliveryDate = null;
    }

    public Requirement Build()
    {
        var requirement = new Requirement()
        {
            Id = _id,
            Quantity = _quantity,
            ClientId = _clientId,
            StorageId = _storageId,
            Status = _status,
            DeliveryDate = _deliveryDate
        };

        Refresh();

        return requirement;
    }

    public RequirementBuilder Completed(DateTime time)
    {
        _deliveryDate = time;
        _status = Status.Completed;
        return this;
    }

    public RequirementBuilder WithClientId(Guid id)
    {
        _clientId = id;
        return this;
    }

    public RequirementBuilder WithStorageId(Guid id)
    {
        _storageId = id;
        return this;
    }
}
