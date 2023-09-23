using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.Domain.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<ExecutionResultGeneric<float>> GetCosts(long from, long to);
    }
}
