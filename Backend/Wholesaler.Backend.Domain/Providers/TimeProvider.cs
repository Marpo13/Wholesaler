using Wholesaler.Backend.Domain.Providers.Interfaces;

namespace Wholesaler.Barckend.Domain.Providers
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}
