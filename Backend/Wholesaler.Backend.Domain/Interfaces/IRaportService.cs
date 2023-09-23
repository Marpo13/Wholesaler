namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IRaportService
    {
        float GetCosts(DateTimeOffset dateFrom, DateTimeOffset dateTo);
    }
}
