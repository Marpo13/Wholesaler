using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Wholesaler.Backend.DataAccess;
using Wholesaler.Backend.Domain.Providers.Interfaces;
using Xunit;

namespace Wholesaler.Tests
{
    public abstract class WholesalerWebTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        protected readonly HttpClient _client;
        protected WholesalerContext _dbContext;
        protected Mock<ITimeProvider> _timeProviderMock = new();

        protected WholesalerWebTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var optionsBuilder = new DbContextOptionsBuilder<WholesalerContext>();
                        optionsBuilder.UseInMemoryDatabase($"WholesalerDb_{Guid.NewGuid()}")
                        .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                        var options = optionsBuilder.Options;

                        var context = new WholesalerContext(options);
                        _dbContext = context;
                                                
                        services.AddSingleton(context);

                        services.AddSingleton(_timeProviderMock.Object);
                    });
                });
                       
            _client = _factory.CreateClient();
        }

        protected void Seed<TValue>(TValue value)
        {
            _dbContext.Add(value);
            _dbContext.SaveChanges();
        }
    }
}
