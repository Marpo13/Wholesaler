using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Backend.DataAccess;

public class TimedHostedService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer? _timer = null;

    public TimedHostedService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new(Update, null, 0, 30000);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void Update(object? state)
    {
        var random = new Random();
        var quantity = random.Next(10, 100);

        var requirement = new Requirement()
        {
            Id = Guid.NewGuid(),
            Quantity = quantity,
            ClientId = new("F1E6AC41-701E-4050-AAAE-5AB32E644A3D"),
            StorageId = new("60DE6110-39FA-45EB-91FA-11AC2B543941"),
            Status = 0,
            DeliveryDate = null
        };

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<WholesalerContext>();
            context.Requirements.Add(requirement);
            context.SaveChanges();
        }
    }
}
