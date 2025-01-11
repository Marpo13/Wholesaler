namespace Wholesaler.Backend.DataAccess.Models;

public class Delivery
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
}
