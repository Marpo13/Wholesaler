namespace Wholesaler.Backend.Domain.Entities;

public class Requirement
{
    public Requirement(Guid id, int quantity, Guid clientId, Guid storageId, Status status, DateTime? deliveryDate)
    {
        Id = id;
        Quantity = quantity;
        ClientId = clientId;
        StorageId = storageId;
        Status = status;
        DeliveryDate = deliveryDate;
    }

    public Requirement(int quantity, Guid clientId, Guid storageId)
    {
        Id = Guid.NewGuid();
        Quantity = quantity;
        ClientId = clientId;
        StorageId = storageId;
        Status = Status.Ongoing;
        DeliveryDate = null;
    }

    public Guid Id { get; }
    public int Quantity { get; private set; }
    public Guid ClientId { get; }
    public Guid StorageId { get; }
    public Status Status { get; private set; }
    public DateTime? DeliveryDate { get; private set; }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void Complete()
    {
        Status = Status.Completed;
    }

    public void SetDate(DateTime time)
    {
        DeliveryDate = time;
    }
}
